namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBecameIncomplete : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressBecameIncomplete(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
