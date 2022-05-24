namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressPostalCodeWasRemoved : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressPostalCodeWasRemoved(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
