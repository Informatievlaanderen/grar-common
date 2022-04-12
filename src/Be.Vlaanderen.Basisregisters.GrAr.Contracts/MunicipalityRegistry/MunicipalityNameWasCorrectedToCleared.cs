namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityNameWasCorrectedToCleared : IMessage
    {
        public string MunicipalityId { get; }

        public string Language { get; }

        public Provenance Provenance { get; }

        public MunicipalityNameWasCorrectedToCleared(
            string municipalityId,
            string language,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Language = language;
            Provenance = provenance;
        }
    }
}
