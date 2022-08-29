namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    using AggregateSource;

    public interface ITestMetadataRepository : IAsyncRepository<TestMetadataAggregate, TestMetadataId> { }
}
