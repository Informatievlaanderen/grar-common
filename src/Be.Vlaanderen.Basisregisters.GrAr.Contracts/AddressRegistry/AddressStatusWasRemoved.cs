namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressStatusWasRemoved : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressStatusWasRemoved(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
