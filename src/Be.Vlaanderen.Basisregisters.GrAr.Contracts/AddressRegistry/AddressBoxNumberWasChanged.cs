namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBoxNumberWasChanged : IQueueMessage
    {
        public string AddressId { get; }

        public string BoxNumber { get; }

        public Provenance Provenance { get; }

        public AddressBoxNumberWasChanged(string addressId,
            string boxNumber,
            Provenance provenance)
        {
            AddressId = addressId;
            BoxNumber = boxNumber;
            Provenance = provenance;
        }
    }
}
