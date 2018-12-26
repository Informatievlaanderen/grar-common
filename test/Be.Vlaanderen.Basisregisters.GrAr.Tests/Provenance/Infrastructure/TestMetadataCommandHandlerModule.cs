namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using System;
    using AggregateSource;
    using CommandHandling;
    using GrAr.Provenance;

    public sealed class TestMetadataCommandHandlerModule : ProvenanceCommandHandlerModule<TestMetadataAggregate>
    {
        public TestMetadataCommandHandlerModule(Func<ITestMetadataRepository> getRepo, Func<ConcurrentUnitOfWork> getUnitOfWork,
            ReturnHandler<CommandMessage> finalHandler = null) : base(getUnitOfWork, finalHandler, new TestMetadataProvenanceFactory())
        {
            For<TestMetadaCommand>().Handle(message =>
            {
                var repo = getRepo();
                var id = new TestMetadataId(1);
                var aggregate = new TestMetadataAggregate();
                aggregate.TestMetadata(message.Command);
                repo.Add(id, aggregate);
            });
        }
    }
}
