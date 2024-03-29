namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    using System.Collections.Generic;
    using AggregateSource;

    public class TestMetadataAggregate : AggregateRootEntity
    {
        public TestMetadataAggregate()
        {
            Register<TestMetadataEvent>(e => { });
        }
        public void TestMetadata(TestMetadataCommand command)
        {
            ApplyChange(new TestMetadataEvent(command.Name));
        }

        #region Metadata
        protected override void BeforeApplyChange(object @event)
        {
            new EventMetadataContext(new Dictionary<string, object>());
            base.BeforeApplyChange(@event);
        }

        #endregion
    }
}
