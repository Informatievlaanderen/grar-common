namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasMigrated : IQueueMessage
    {
        public string AddressId { get; }

        public int StreetNamePersistentLocalId { get; set; }

        public Provenance Provenance { get; }

        public AddressWasMigrated(string addressId,
            int streetNamePersistentLocalId,
            Provenance provenance)
        {
            AddressId = addressId;
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            Provenance = provenance;
        }
    }
}
