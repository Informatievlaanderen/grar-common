namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class StreetNameWasRemoved : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRemoved(int streetNamePersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            Provenance = provenance;
        }
    }
}
