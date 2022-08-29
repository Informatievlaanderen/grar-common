namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    using AggregateSource;
    using Be.Vlaanderen.Basisregisters.AggregateSource.SqlStreamStore;
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
