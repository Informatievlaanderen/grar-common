namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressStreetNameWasChanged : IQueueMessage
    {
        public string AddressId { get; }

        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public AddressStreetNameWasChanged(string addressId,
            string streetNameId,
            Provenance provenance)
        {
            AddressId = addressId;
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
