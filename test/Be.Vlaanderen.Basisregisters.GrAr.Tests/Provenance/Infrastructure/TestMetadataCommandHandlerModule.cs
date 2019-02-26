namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using System;
    using AggregateSource;
    using CommandHandling;
    using GrAr.Provenance;

    public sealed class TestMetadataCommandHandlerModule : CommandHandlerModule
    {
        public TestMetadataCommandHandlerModule(
            Func<ITestMetadataRepository> getRepo,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            TestMetadataProvenanceFactory provenanceFactory)
        {
            For<TestMetadaCommand>()
                .AddProvenance(getUnitOfWork, provenanceFactory)
                .Handle(message =>
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
