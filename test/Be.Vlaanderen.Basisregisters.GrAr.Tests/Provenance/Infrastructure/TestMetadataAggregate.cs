namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using AggregateSource;

    public class TestMetadataAggregate : AggregateRootEntity
    {
        public TestMetadataAggregate()
        {
            this.Register<TestMetadataEvent>(e => { });
        }
        public void TestMetadata(TestMetadaCommand command)
        {
            ApplyChange(new TestMetadataEvent());
        }
    }
}
