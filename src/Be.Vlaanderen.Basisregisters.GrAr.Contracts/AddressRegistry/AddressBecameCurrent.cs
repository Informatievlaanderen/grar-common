namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBecameCurrent : IQueueMessage
    {
        public string AddressId { get; }

        public Provenance Provenance { get; }

        public AddressBecameCurrent(string addressId,
            Provenance provenance)
        {
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
