namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;
    using Contracts;

    public class StreetNameWasCorrectedFromRetiredToCurrent : IQueueMessage
    {
        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasCorrectedFromRetiredToCurrent(
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
