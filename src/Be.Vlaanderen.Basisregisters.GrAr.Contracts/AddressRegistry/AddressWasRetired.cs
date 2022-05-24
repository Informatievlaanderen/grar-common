namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRetired : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressWasRetired(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
