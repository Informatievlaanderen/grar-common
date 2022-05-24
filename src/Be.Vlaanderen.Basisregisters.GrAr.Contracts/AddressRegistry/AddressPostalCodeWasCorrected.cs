namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressPostalCodeWasCorrected : IQueueMessage
    {
        public string AddressId { get; }

        public string PostalCode { get; }

        public Provenance Provenance { get; }

        public AddressPostalCodeWasCorrected(string addressId,
            string postalCode,
            Provenance provenance)
        {
            AddressId = addressId;
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
