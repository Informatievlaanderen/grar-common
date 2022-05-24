namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBecameNotOfficiallyAssigned : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressBecameNotOfficiallyAssigned(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
