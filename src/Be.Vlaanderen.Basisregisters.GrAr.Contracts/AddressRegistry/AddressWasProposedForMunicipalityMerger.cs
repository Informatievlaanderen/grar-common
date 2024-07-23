namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasProposedForMunicipalityMerger : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int? ParentPersistentLocalId { get; }

        public string DesiredStatus { get; set; }

        public string PostalCode { get; }

        public string HouseNumber { get; }

        public string? BoxNumber { get; }

        public string GeometryMethod { get; }

        public string GeometrySpecification { get; }

        public string ExtendedWkbGeometry { get; }

        public bool OfficiallyAssigned { get; }

        public int MergedAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public AddressWasProposedForMunicipalityMerger(int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            int? parentPersistentLocalId,
            string desiredStatus,
            string postalCode,
            string houseNumber,
            string? boxNumber,
            string geometryMethod,
            string geometrySpecification,
            string extendedWkbGeometry,
            bool officiallyAssigned,
            int mergedAddressPersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            ParentPersistentLocalId = parentPersistentLocalId;
            DesiredStatus = desiredStatus;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            GeometryMethod = geometryMethod;
            GeometrySpecification = geometrySpecification;
            ExtendedWkbGeometry = extendedWkbGeometry;
            OfficiallyAssigned = officiallyAssigned;
            MergedAddressPersistentLocalId = mergedAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
