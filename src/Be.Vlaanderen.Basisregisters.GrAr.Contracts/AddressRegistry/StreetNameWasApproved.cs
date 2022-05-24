namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class StreetNameWasApproved : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasApproved(int streetNamePersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            Provenance = provenance;
        }
    }
}
