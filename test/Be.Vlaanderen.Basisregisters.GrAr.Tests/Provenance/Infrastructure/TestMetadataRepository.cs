namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using AggregateSource;
    using AggregateSource.SqlStreamStore;
    using EventHandling;
    using SqlStreamStore;

    public class TestMetadataRepository : Repository<TestMetadataAggregate, TestMetadataId>, ITestMetadataRepository
    {
        public TestMetadataRepository(ConcurrentUnitOfWork unitOfWork, IStreamStore eventStore, EventMapping eventMapping, EventDeserializer eventDeserializer)
            : base(() => new TestMetadataAggregate(), unitOfWork, eventStore, eventMapping, eventDeserializer)
        {
        }
    }
}
