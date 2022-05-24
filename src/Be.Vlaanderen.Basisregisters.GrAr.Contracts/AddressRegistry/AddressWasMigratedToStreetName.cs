namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasMigratedToStreetName : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public string AddressId { get; }

        public string StreetNameId { get; }

        public int AddressPersistentLocalId { get; }

        public string Status { get; }

        public string HouseNumber { get; }

        public string? BoxNumber { get; }

        public string GeometryMethod { get; }

        public string GeometrySpecification { get; }

        public string ExtendedWkbGeometry { get; }

        public bool OfficiallyAssigned { get; }

        public string? PostalCode { get; }

        public bool IsCompleted { get; }

        public bool IsRemoved { get; }

        public int? ParentPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public AddressWasMigratedToStreetName(int streetNamePersistentLocalId,
            string addressId,
            string streetNameId,
            int addressPersistentLocalId,
            string status,
            string houseNumber,
            string? boxNumber,
            string geometryMethod,
            string geometrySpecification,
            string extendedWkbGeometry,
            bool officiallyAssigned,
            string? postalCode,
            bool isCompleted,
            bool isRemoved,
            int? parentPersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressId = addressId;
            StreetNameId = streetNameId;
            AddressPersistentLocalId = addressPersistentLocalId;
            Status = status;
            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            GeometryMethod = geometryMethod;
            GeometrySpecification = geometrySpecification;
            ExtendedWkbGeometry = extendedWkbGeometry;
            OfficiallyAssigned = officiallyAssigned;
            PostalCode = postalCode;
            IsCompleted = isCompleted;
            IsRemoved = isRemoved;
            ParentPersistentLocalId = parentPersistentLocalId;
            Provenance = provenance;
        }
    }
}
