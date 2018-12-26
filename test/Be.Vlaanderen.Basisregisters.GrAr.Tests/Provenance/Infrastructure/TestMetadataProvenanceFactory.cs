namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using System;
    using GrAr.Provenance;

    public class TestMetadataProvenanceFactory : CrabProvenanceFactory, IProvenanceFactory<TestMetadataAggregate>
    {
        public bool CanCreateFrom<TCommand>()
        {
            return typeof(IHasCrabProvenance).IsAssignableFrom(typeof(TCommand));
        }

        public Provenance CreateFrom(object provenanceHolder,
            TestMetadataAggregate aggregate)
        {
            if (!(provenanceHolder is IHasCrabProvenance crabProvenance))
                throw new ApplicationException($"Cannot create provenance from {provenanceHolder.GetType().Name}");

            return CreateFrom(0, false, crabProvenance.Timestamp, crabProvenance.Modification, crabProvenance.Operator, crabProvenance.Organisation);
        }
    }
}
