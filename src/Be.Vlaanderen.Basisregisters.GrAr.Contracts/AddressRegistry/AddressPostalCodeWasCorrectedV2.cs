namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressPostalCodeWasCorrectedV2 : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public string PostalCode { get; }

        public Provenance Provenance { get; }

        public AddressPostalCodeWasCorrectedV2(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            string postalCode,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
