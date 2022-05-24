namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasOfficiallyAssigned : IQueueMessage
    {
        public string AddressId { get; }
        
        public Provenance Provenance { get; }

        public AddressWasOfficiallyAssigned(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
