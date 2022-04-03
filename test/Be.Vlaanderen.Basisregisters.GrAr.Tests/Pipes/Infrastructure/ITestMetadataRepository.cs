namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Pipes.Infrastructure
{
    using AggregateSource;

    public interface ITestMetadataRepository : IAsyncRepository<TestMetadataAggregate, TestMetadataId> { }
}
