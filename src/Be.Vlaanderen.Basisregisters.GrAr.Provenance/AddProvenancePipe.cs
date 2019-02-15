namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System;
    using System.Linq;
    using AggregateSource;
    using CommandHandling;

    public static class AddProvenancePipe
    {
        public static void AddProvenance<TCommand, TAggregate>(Func<ConcurrentUnitOfWork> getUnitOfWork,
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

        internal static ICommandHandlerBuilder<CommandMessage<TCommand>> AddProvenance<TCommand, TAggregate>(
            this ICommandHandlerBuilder<CommandMessage<TCommand>> commandHandlerBuilder,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            Func<TCommand, TAggregate, Provenance> provenanceFactory)
            where TAggregate : IAggregateRootEntity
        {
            return commandHandlerBuilder.Pipe(next => async (commandMessage, ct) =>
            {
                var handler = await next(commandMessage, ct);

                AddProvenance(getUnitOfWork, commandMessage, provenanceFactory);

                return handler;
            });
        }
    }
}
