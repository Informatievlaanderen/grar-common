namespace Be.Vlaanderen.Basisregisters.GrAr.Provenance
{
    public interface IHasProvenance
    {
        ProvenanceData Provenance { get; }
    }

    public interface ISetProvenance
    {
        void SetProvenance(Provenance provenance);
    }
}
