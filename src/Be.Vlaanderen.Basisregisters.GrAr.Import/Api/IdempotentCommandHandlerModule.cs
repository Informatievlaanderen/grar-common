namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Api
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AggregateSource;
    using AggregateSource.Snapshotting;
    using Autofac;
    using CommandHandling;
    using CommandHandling.Idempotency;
    using EventHandling;
    using Generators.Guid;
    using Microsoft.EntityFrameworkCore;
    using SqlStreamStore;
    using SqlStreamStore.Streams;
    using Utilities.HexByteConvertor;

    public partial class IdempotentCommandHandlerModule
    {
        private int _maxSqlInSize = 2000;
        protected ILifetimeScope Container { get; }
        private const int AggregateExpectedVersionNotSet = -1000;
        private const int IdempotencyCommandTimeoutInSeconds = 120;

        public IdempotentCommandHandlerModule(ILifetimeScope container)
        {
            Container = container;
        }

        public async Task<long?> IdempotentCommandHandlerDispatchBatch(
            Dictionary<Guid?, dynamic> commands,
            IDictionary<string, object> metadata = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            using (var c = Container.BeginLifetimeScope())
            {
                // GENERIC
                var concurrentUnitOfWork = c.Resolve<ConcurrentUnitOfWork>();
                var getStreamStore = c.Resolve<Func<IStreamStore>>();
                var eventMapping = c.Resolve<EventMapping>();
                var eventSerializer = c.Resolve<EventSerializer>();
                var context = c.Resolve<IdempotencyContext>();

                context.Database.SetCommandTimeout(IdempotencyCommandTimeoutInSeconds);

                // WHICH COMMANDS SHOULD I PROCESS?
                if (commands.Any(x => !x.Key.HasValue))
                    throw new InvalidCommandException("Ongeldig verzoek id.");

                var validCommands = commands
                    .Where(x => x.Key.HasValue)
                    .ToDictionary(x => x.Key.Value, x => new CommandContainer(x.Key.Value, x.Value));

                var commandIds = validCommands.Keys.ToList();

                Dictionary<Guid, string> possibleProcessedCommands;
                if (commandIds.Count < _maxSqlInSize)
                {
                    possibleProcessedCommands = await context.ProcessedCommands
                        .Where(x => commandIds.Contains(x.CommandId))
                        .ToDictionaryAsync(x => x.CommandId, x => x.CommandContentHash, cancellationToken);
                }
                else
                {
                    var tempPossibleProcessedCommands = new List<ProcessedCommand>();
                    for (var i = 0; i < commandIds.Count; i += _maxSqlInSize)
                    {
                        var ids = commandIds.Skip(i).Take(_maxSqlInSize);
                        tempPossibleProcessedCommands.AddRange(await context.ProcessedCommands
                            .Where(x => ids.Contains(x.CommandId))
                            .ToListAsync(cancellationToken));
                    }

                    possibleProcessedCommands = tempPossibleProcessedCommands
                        .Where(x => commandIds.Contains(x.CommandId))
                        .ToDictionary(x => x.CommandId, x => x.CommandContentHash);
                }

                var certainlyProcessedCommandIds = possibleProcessedCommands
                    .Where(x => validCommands[x.Key].ContentHash == x.Value)
                    .Select(x => x.Key)
                    .ToList();

                var commandIdsNotYetProcessed = commandIds.Except(certainlyProcessedCommandIds);
                var commandsToProcess = validCommands
                    .Where(x => commandIdsNotYetProcessed.Contains(x.Key))
                    .Select(x => x.Value)
                    .ToList();

                if (commandsToProcess.Count == 0)
                    return null;

                try
                {
                    // Store commandIds in Command Store if it does not exist
                    foreach (var commandToProcess in commandsToProcess)
                        await context.ProcessedCommands.AddAsync(commandToProcess.ProcessedCommand, cancellationToken);

                    await context.SaveChangesAsync(cancellationToken);

                    var position = 0;
                    var aggregateIdentifier = "";
                    var aggregateExpectedVersion = AggregateExpectedVersionNotSet;
                    var changes = new List<Tuple<Guid, List<object>, IDictionary<string, object>>>();
                    Aggregate? latestAggregate = null;

                    var processor = c.Resolve<IIdempotentCommandHandlerModuleProcessor>();

                    foreach (var commandToProcess in commandsToProcess.Select(x => x.Command))
                    {
                        Guid commandId = commandToProcess.CreateCommandId();
                        CommandMessage commandMessage = await processor.Process(commandToProcess, metadata, position, cancellationToken);

                        var aggregate = concurrentUnitOfWork.GetChanges().SingleOrDefault();
                        if (aggregate != null)
                        {
                            var events = aggregate.Root.GetChanges().Skip(position).ToList();
                            position += events.Count;

                            var commandMetadata = commandMessage?.Metadata ?? new Dictionary<string, object>();
                            if (!commandMetadata.ContainsKey("commandId"))
                                commandMetadata.Add("commandId", commandId);

                            changes.Add(Tuple.Create(commandId, events, commandMetadata));

                            aggregateIdentifier = aggregate.Identifier;
                            aggregateExpectedVersion = aggregate.ExpectedVersion;
                            latestAggregate = aggregate;
                        }
                    }

                    if (!changes.Any())
                        return -1L;

                    var i = 1;
                    var messages = changes
                        .SelectMany(tuple =>
                            tuple.Item2.Select(@event =>
                                new NewStreamMessage(
                                    messageId: Deterministic.Create(Deterministic.Namespaces.Events, $"{tuple.Item1}-{i++}"),
                                    type: eventMapping.GetEventName(@event.GetType()),
                                    jsonData: eventSerializer.SerializeObject(@event),
                                    jsonMetadata: eventSerializer.SerializeObject(tuple.Item3))))
                        .ToArray();

                    var streamStore = getStreamStore();

                    var result = await streamStore.AppendToStream(
                        aggregateIdentifier,
                        aggregateExpectedVersion,
                        messages,
                        cancellationToken);

                    var snapshotable = latestAggregate?.Root as ISnapshotable;
                    if (snapshotable != null)
                    {
                        await CreateSnapshot(
                            snapshotable,
                            new SnapshotStrategyContext(
                                latestAggregate,
                                changes
                                    .SelectMany(e => e.Item2)
                                    .Select(e => e is EventWithMetadata ? e : new EventWithMetadata(e))
                                    .Cast<EventWithMetadata>()
                                    .ToImmutableList(),
                                result.CurrentVersion),
                            streamStore,
                            concurrentUnitOfWork,
                            eventMapping,
                            eventSerializer,
                            cancellationToken);
                    }

                    return result.CurrentPosition;
                }
                catch
                {
                    // On exception, remove commandIds from Command Store
                    foreach (var commandToProcess in commandsToProcess)
                        context.ProcessedCommands.Remove(commandToProcess.ProcessedCommand);

                    context.SaveChanges();

                    throw;
                }
            }
        }

        private static async Task CreateSnapshot(
            ISnapshotable snapshotSupport,
            SnapshotStrategyContext context,
            IStreamStore streamStore,
            ConcurrentUnitOfWork uow,
            EventMapping eventMapping,
            EventSerializer eventSerializer,
            CancellationToken ct)
        {
            if (!snapshotSupport.Strategy.ShouldCreateSnapshot(context))
                return;

            var snapshot = snapshotSupport.TakeSnapshot();
            if (snapshot == null)
                throw new InvalidOperationException("Snapshot missing.");

            var snapshotContainer = new SnapshotContainer
            {
                Data = eventSerializer.SerializeObject(snapshot),
                Info =
                {
                    Type = eventMapping.GetEventName(snapshot.GetType()),
                    StreamVersion = context.StreamVersion
                }
            };

            await streamStore.AppendToStream(
                uow.GetSnapshotIdentifier(context.Aggregate.Identifier),
                ExpectedVersion.Any,
                new NewStreamMessage(
                    Deterministic.Create(Deterministic.Namespaces.Events, $"snapshot-{context.Aggregate.Identifier}-{context.StreamVersion}"),
                    $"SnapshotContainer<{snapshotContainer.Info.Type}>",
                    eventSerializer.SerializeObject(snapshotContainer)),
                ct);
        }

        private class CommandContainer
        {
            public string ContentHash { get; }

            public dynamic Command { get; }

            public ProcessedCommand ProcessedCommand { get; }

            public CommandContainer(Guid commandId, dynamic command)
            {
                Command = command;

                ContentHash = SHA512
                    .Create()
                    .ComputeHash(Encoding.UTF8.GetBytes((string)command.ToString()))
                    .ToHexString();

                ProcessedCommand = new ProcessedCommand(commandId, ContentHash);
            }
        }
    }
}
