namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.Infrastructure
{
    using System;
    using Crab;
    using GrAr.Provenance;

    public class TestMetadaCommand : IHasCrabProvenance
    {
        public CrabTimestamp Timestamp { get; } = new CrabTimestamp(DateTime.Now.ToCrabInstant());
        public CrabOperator Operator { get; } = new CrabOperator("metadatatest");
        public CrabModification? Modification { get; } = CrabModification.Insert;
        public CrabOrganisation? Organisation { get; } = CrabOrganisation.Other;
    }
}
