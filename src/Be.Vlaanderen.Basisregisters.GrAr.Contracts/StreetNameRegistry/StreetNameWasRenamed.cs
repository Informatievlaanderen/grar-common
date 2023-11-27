namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasRenamed : IQueueMessage
    {
        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }
        public int DestinationPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRenamed(
            string municipalityId,
            int persistentLocalId,
            int destinationPersistentLocalId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            DestinationPersistentLocalId = destinationPersistentLocalId;
            Provenance = provenance;
        }
    }
}
