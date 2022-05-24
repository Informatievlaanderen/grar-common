namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressHouseNumberWasCorrected : IQueueMessage
    {
        public string AddressId { get; }

        public string HouseNumber { get; }

        public Provenance Provenance { get; }

        public AddressHouseNumberWasCorrected(string addressId,
            string houseNumber,
            Provenance provenance)
        {
            AddressId = addressId;
            HouseNumber = houseNumber;
            Provenance = provenance;
        }
    }
}
