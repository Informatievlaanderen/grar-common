namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressStatusWasCorrectedToRemoved : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressStatusWasCorrectedToRemoved(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
