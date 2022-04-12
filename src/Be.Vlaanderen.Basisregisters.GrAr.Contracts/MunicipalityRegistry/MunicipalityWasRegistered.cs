namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasRegistered : IMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public Provenance Provenance { get; }

        public MunicipalityWasRegistered(
            string municipalityId,
            string nisCode,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            Provenance = provenance;
        }
    }
}
