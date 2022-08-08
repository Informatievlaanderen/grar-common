namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class StreetNameWasRetired : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRetired(int streetNamePersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            Provenance = provenance;
        }
    }
}
