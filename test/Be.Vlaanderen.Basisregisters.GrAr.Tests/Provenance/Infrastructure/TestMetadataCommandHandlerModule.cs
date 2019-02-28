namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using System;
    using AggregateSource;
    using CommandHandling;
    using CommandHandling.SqlStreamStore;
    using EventHandling;
    using GrAr.Provenance;
    using SqlStreamStore;

    public sealed class TestMetadataCommandHandlerModule : CommandHandlerModule
    {
        public TestMetadataCommandHandlerModule(
            Func<ITestMetadataRepository> getRepo,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            Func<IStreamStore> getStreamStore,
            EventMapping eventMapping,
            EventSerializer eventSerializer,
            TestMetadataProvenanceFactory provenanceFactory)
        {
            For<TestMetadaCommand>()
                .AddSqlStreamStore(getStreamStore, getUnitOfWork, eventMapping, eventSerializer)
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
