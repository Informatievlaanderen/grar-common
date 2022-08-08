namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRejectedBecauseHouseNumberWasRejected : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public Provenance Provenance { get; }


        public AddressWasRejectedBecauseHouseNumberWasRejected(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
