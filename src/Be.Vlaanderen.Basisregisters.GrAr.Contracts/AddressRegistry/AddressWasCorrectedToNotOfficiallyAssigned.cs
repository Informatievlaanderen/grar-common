namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasCorrectedToNotOfficiallyAssigned : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressWasCorrectedToNotOfficiallyAssigned(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
