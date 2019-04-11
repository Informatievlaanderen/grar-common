namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using AggregateSource;
    using CommandHandling;
    using System;
    using System.Linq;

    public static class AddProvenancePipe
    {
        public static ICommandHandlerBuilder<CommandMessage<TCommand>> AddProvenance<TCommand, TAggregate>(
            this ICommandHandlerBuilder<CommandMessage<TCommand>> commandHandlerBuilder,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            IProvenanceFactory<TAggregate>[] provenanceFactories)
            where TAggregate : IAggregateRootEntity
        {
            var provenanceFactory = provenanceFactories.SingleOrDefault(f => f.CanCreateFrom<TCommand>());
            return provenanceFactory == null
                ? commandHandlerBuilder
                : commandHandlerBuilder.AddProvenance(getUnitOfWork, provenanceFactory);
        }

        public static ICommandHandlerBuilder<CommandMessage<TCommand>> AddProvenance<TCommand, TAggregate>(
            this ICommandHandlerBuilder<CommandMessage<TCommand>> commandHandlerBuilder,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            IProvenanceFactory<TAggregate> provenanceFactory)
            where TAggregate : IAggregateRootEntity
        {
            return provenanceFactory == null
                ? commandHandlerBuilder
                : commandHandlerBuilder.AddProvenance<TCommand, TAggregate>(getUnitOfWork, (m, a) => provenanceFactory.CreateFrom(m, a));
        }

        public static ICommandHandlerBuilder<CommandMessage<TCommand>> AddProvenance<TCommand, TAggregate>(
            this ICommandHandlerBuilder<CommandMessage<TCommand>> commandHandlerBuilder,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            Func<TCommand, TAggregate, Provenance> provenanceFactory)
            where TAggregate : IAggregateRootEntity
        {
            return commandHandlerBuilder.Pipe(next => async (commandMessage, ct) =>
            {
                var result = await next(commandMessage, ct);

                AddProvenance(getUnitOfWork, commandMessage, provenanceFactory);

                return result;
            });
        }

        public static void AddProvenance<TCommand, TAggregate>(
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            CommandMessage<TCommand> message,
            Func<TCommand, TAggregate, Provenance> provenanceFactory)
            where TAggregate : IAggregateRootEntity
        {
            var aggregates = getUnitOfWork()
                .GetChanges()
                .Select(aggregate => aggregate.Root)
                .OfType<TAggregate>();

            foreach (var aggregate in aggregates)
            {
                var events = aggregate
                    .GetChanges()
                    .OfType<ISetProvenance>();

                var provenance = provenanceFactory(message.Command, aggregate);

                message.AddMetadata(Provenance.ProvenanceMetadataKey, provenance.ToDictionary());

                foreach (var @event in events)
                    @event.SetProvenance(provenance);
            }
        }

        public static void AddProvenance<TCommand, TAggregate>(
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            CommandMessage<TCommand> message,
            Func<TCommand, TAggregate, Provenance> provenanceFactory,
            int fromPosition)
            where TAggregate : IAggregateRootEntity
        {
            var aggregateRoot = getUnitOfWork()
                .GetChanges()
                .Select(aggregate => aggregate.Root)
                .OfType<TAggregate>()
                .Single();

            var events = aggregateRoot
                .GetChanges()
                .Skip(fromPosition)
                .OfType<ISetProvenance>();

            var provenance = provenanceFactory(message.Command, aggregateRoot);

            message.AddMetadata(Provenance.ProvenanceMetadataKey, provenance.ToDictionary());

            foreach (var @event in events)
                @event.SetProvenance(provenance);
        }
    }
}
