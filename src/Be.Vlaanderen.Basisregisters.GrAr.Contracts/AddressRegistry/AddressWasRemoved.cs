namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRemoved : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressWasRemoved(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
