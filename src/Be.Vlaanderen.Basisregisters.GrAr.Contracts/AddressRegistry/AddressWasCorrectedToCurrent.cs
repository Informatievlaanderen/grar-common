namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasCorrectedToCurrent : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressWasCorrectedToCurrent(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
