namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using AggregateSource;

    public interface ITestMetadataRepository : IAsyncRepository<TestMetadataAggregate, TestMetadataId> { }
}
