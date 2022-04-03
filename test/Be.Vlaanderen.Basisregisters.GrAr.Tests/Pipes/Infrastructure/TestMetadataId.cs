namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Pipes.Infrastructure
{
    using AggregateSource;
    using Newtonsoft.Json;

    public class TestMetadataId : IntegerValueObject<TestMetadataId>
    {
        public TestMetadataId([JsonProperty("value")] int testMetadataId) : base(testMetadataId) { }
    }
}
