namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressHouseNumberWasChanged : IQueueMessage
    {
        public string AddressId { get; }

        public string HouseNumber { get; }

        public Provenance Provenance { get; }

        public AddressHouseNumberWasChanged(string addressId,
            string houseNumber,
            Provenance provenance)
        {
            AddressId = addressId;
            HouseNumber = houseNumber;
            Provenance = provenance;
        }
    }
}
