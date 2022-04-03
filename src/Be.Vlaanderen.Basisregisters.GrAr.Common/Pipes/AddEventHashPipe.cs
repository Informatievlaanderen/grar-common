namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Pipes
{
    using System;
    using System.Linq;
    using AggregateSource;
    using CommandHandling;

    public static class AddEventHashPipe
    {
        public const string HashMetadataKey = "EventHash";

        public static ICommandHandlerBuilder<CommandMessage<TCommand>> AddEventHash<TCommand, TAggregate>(
            this ICommandHandlerBuilder<CommandMessage<TCommand>> commandHandlerBuilder,
            Func<ConcurrentUnitOfWork> getUnitOfWork)
            where TAggregate : IAggregateRootEntity
        {
            return commandHandlerBuilder.Pipe(next => async (commandMessage, ct) =>
            {
                var result = await next(commandMessage, ct);

                AddEventHash<TAggregate>(getUnitOfWork);

                return result;
            });
        }

        public static void AddEventHash<TAggregate>(
            Func<ConcurrentUnitOfWork> getUnitOfWork)
            where TAggregate : IAggregateRootEntity
        {
            var aggregates = getUnitOfWork()
                .GetChanges()
                .Select(aggregate => aggregate.Root)
                .OfType<TAggregate>();

            foreach (var aggregate in aggregates)
            {
                var events = aggregate
                    .GetChangesWithMetadata();

                foreach (var eventWithMetadata in events)
                {
                    if (eventWithMetadata.Event is IHaveHash @event)
                    {
                        eventWithMetadata.Metadata[HashMetadataKey] = @event.GetHash();
                    }
                }
            }
        }
    }
}
