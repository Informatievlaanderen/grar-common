namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasProposedV2 : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int? ParentPersistentLocalId { get; }

        public string PostalCode { get; }

        public string HouseNumber { get; }

        public string? BoxNumber { get; }

        public Provenance Provenance { get; }

        public AddressWasProposedV2(int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            int? parentPersistentLocalId,
            string postalCode,
            string houseNumber,
            string? boxNumber,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            ParentPersistentLocalId = parentPersistentLocalId;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            Provenance = provenance;
        }
    }
}
