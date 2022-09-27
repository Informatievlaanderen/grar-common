namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressBoxNumberWasCorrectedV2 : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public string BoxNumber { get; }

        public Provenance Provenance { get; }

        public AddressBoxNumberWasCorrectedV2(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            string boxNumber,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            BoxNumber = boxNumber;
            Provenance = provenance;
        }
    }
}
