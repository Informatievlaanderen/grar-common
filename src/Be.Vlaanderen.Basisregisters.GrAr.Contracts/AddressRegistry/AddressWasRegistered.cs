namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRegistered : IQueueMessage
    {
        public string AddressId { get; }

        public string StreetNameId { get; }

        public string HouseNumber { get; }

        public Provenance Provenance { get; }

        public AddressWasRegistered(string addressId,
            string streetNameId,
            string houseNumber,
            Provenance provenance)
        {
            AddressId = addressId;
            StreetNameId = streetNameId;
            HouseNumber = houseNumber;
            Provenance = provenance;
        }
    }
}
