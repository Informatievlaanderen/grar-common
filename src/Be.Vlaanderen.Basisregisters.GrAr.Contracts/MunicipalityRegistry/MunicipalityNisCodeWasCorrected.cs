namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityNisCodeWasCorrected
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public Provenance Provenance { get; }

        public MunicipalityNisCodeWasCorrected(
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
