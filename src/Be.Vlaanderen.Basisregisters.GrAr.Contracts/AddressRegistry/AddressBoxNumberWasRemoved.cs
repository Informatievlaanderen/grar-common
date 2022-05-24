namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBoxNumberWasRemoved : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressBoxNumberWasRemoved(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
