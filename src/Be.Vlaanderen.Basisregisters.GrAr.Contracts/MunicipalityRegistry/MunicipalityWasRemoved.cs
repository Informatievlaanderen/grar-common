namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasRemoved : IQueueMessage
    {
        public string MunicipalityId { get; }

        public Provenance Provenance { get; }

        public MunicipalityWasRemoved(
            string municipalityId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Provenance = provenance;
        }
    }
}
