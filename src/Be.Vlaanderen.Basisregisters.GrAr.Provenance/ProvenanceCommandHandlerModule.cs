namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using System;
    using System.Linq;
    using AggregateSource;
    using CommandHandling;

    public class ProvenanceCommandHandlerModule<TAggregate> : CommandHandlerModule where TAggregate : IAggregateRootEntity
    {
        private readonly Func<ConcurrentUnitOfWork> _getUnitOfWork;
        private readonly IProvenanceFactory<TAggregate>[] _provenanceFactories;

        public ProvenanceCommandHandlerModule(Func<ConcurrentUnitOfWork> getUnitOfWork,
            ReturnHandler<CommandMessage> finalHandler = null,
            params IProvenanceFactory<TAggregate>[] provenanceFactories) : base(finalHandler)
        {
            _provenanceFactories = provenanceFactories;
            _getUnitOfWork = getUnitOfWork;
        }

        public override ICommandHandlerBuilder<CommandMessage<TCommand>> For<TCommand>()
        {
            var provenanceFactory = _provenanceFactories.SingleOrDefault(f => f.CanCreateFrom<TCommand>());
            if (provenanceFactory == null)
                return base.For<TCommand>();
            return base.For<TCommand>()
                .AddProvenance<TCommand, TAggregate>(_getUnitOfWork, provenanceFactory.CreateFrom);
        }
    }
}
