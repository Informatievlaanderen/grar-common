namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    using Crab;

    public interface IHasCrabProvenance
    {
        CrabTimestamp Timestamp { get; }
        CrabOperator Operator { get; }
        CrabModification? Modification { get; }
        CrabOrganisation? Organisation { get; }
    }
}
