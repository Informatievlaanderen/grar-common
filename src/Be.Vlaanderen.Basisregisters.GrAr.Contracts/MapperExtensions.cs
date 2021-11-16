namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts
{
    using Common;
    using GrAr.Common;
    using GrAr = GrAr;

    public static class MapperExtensions
    {
        public static Provenance ToContract(this GrAr.Provenance.Provenance provenance)
        {
            return new(
                provenance.Timestamp.ToIso8601(),
                provenance.Application.ToString(),
                provenance.Modification.ToString(),
                provenance.Operator.ToString(),
                provenance.Organisation.ToString(),
                provenance.Reason.ToString());
        }

        public static Provenance ToContract(this GrAr.Provenance.ProvenanceData provenance)
        {
            return new(
                provenance.Timestamp.ToIso8601(),
                provenance.Application.ToString(),
                provenance.Modification.ToString(),
                provenance.Operator,
                provenance.Organisation.ToString(),
                provenance.Reason);
        }
    }
}
