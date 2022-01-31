namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityFacilityLanguageWasRemoved : IQueueMessage
    {
        public string MunicipalityId { get; }

        public string Language { get; }

        public Provenance Provenance { get; }

        public MunicipalityFacilityLanguageWasRemoved(
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
