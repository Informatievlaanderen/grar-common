namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBecameComplete : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressBecameComplete(
            string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
