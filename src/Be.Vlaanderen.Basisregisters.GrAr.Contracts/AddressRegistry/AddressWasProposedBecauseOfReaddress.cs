namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasProposedBecauseOfReaddress : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int SourceAddressPersistentLocalId { get; }

        public int? ParentPersistentLocalId { get; }

        public string PostalCode { get; }

        public string HouseNumber { get; }

        public string? BoxNumber { get; }

        public string GeometryMethod { get; }

        public string GeometrySpecification { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public AddressWasProposedBecauseOfReaddress(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            int sourceAddressPersistentLocalId,
            int? parentPersistentLocalId,
            string postalCode,
            string houseNumber,
            string? boxNumber,
            string geometryMethod,
            string geometrySpecification,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            ParentPersistentLocalId = parentPersistentLocalId;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            GeometryMethod = geometryMethod;
            GeometrySpecification = geometrySpecification;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
