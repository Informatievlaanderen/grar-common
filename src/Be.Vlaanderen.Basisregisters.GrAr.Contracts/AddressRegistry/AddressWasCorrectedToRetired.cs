namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasCorrectedToRetired : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressWasCorrectedToRetired(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
