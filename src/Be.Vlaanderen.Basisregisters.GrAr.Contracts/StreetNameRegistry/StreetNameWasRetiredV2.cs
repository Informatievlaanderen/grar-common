namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;
    using Contracts;

    public class StreetNameWasRetiredV2 : IQueueMessage
    {
        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRetiredV2(
            string municipalityId,
            int persistentLocalId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;    
        }
    }
}
