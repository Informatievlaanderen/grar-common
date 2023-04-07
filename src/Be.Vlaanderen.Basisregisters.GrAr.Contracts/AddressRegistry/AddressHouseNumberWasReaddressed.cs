namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using Common;

    public class AddressHouseNumberWasReaddressed : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public ReaddressedAddressData ReaddressedHouseNumber { get; }

        public IReadOnlyList<ReaddressedAddressData> ReaddressedBoxNumbers { get; }

        public IReadOnlyList<int> RejectedBoxNumberAddressPersistentLocalIds { get; }

        public IReadOnlyList<int> RetiredBoxNumberAddressPersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public AddressHouseNumberWasReaddressed(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            ReaddressedAddressData readdressedHouseNumber,
            IReadOnlyList<ReaddressedAddressData> readdressedBoxNumbers,
            IReadOnlyList<int> rejectedBoxNumberAddressPersistentLocalIds,
            IReadOnlyList<int> retiredBoxNumberAddressPersistentLocalIds,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            ReaddressedHouseNumber = readdressedHouseNumber;
            ReaddressedBoxNumbers = readdressedBoxNumbers;
            RejectedBoxNumberAddressPersistentLocalIds = rejectedBoxNumberAddressPersistentLocalIds;
            RetiredBoxNumberAddressPersistentLocalIds = retiredBoxNumberAddressPersistentLocalIds;
            Provenance = provenance;
        }
    }

    public class ReaddressedAddressData
    {
        public int SourceAddressPersistentLocalId { get; }
        public int DestinationAddressPersistentLocalId { get; }
        public string SourceStatus { get; }
        public string DestinationHouseNumber { get; }
        public string? SourceBoxNumber { get; }
        public string SourcePostalCode { get; }
        public string SourceGeometryMethod { get; }
        public string SourceGeometrySpecification { get; }
        public string SourceExtendedWkbGeometry { get; }
        public bool SourceIsOfficiallyAssigned { get; }
        public int? DestinationParentAddressPersistentLocalId { get; }

        public ReaddressedAddressData(int sourceAddressPersistentLocalId,
            int destinationAddressPersistentLocalId,
            string sourceStatus,
            string destinationHouseNumber,
            string? sourceBoxNumber,
            string sourcePostalCode,
            string sourceGeometryMethod,
            string sourceGeometrySpecification,
            string sourceExtendedWkbGeometry,
            bool sourceIsOfficiallyAssigned,
            int? destinationParentAddressPersistentLocalId)
        {
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
            SourceStatus = sourceStatus;
            DestinationHouseNumber = destinationHouseNumber;
            SourceBoxNumber = sourceBoxNumber;
            SourcePostalCode = sourcePostalCode;
            SourceGeometryMethod = sourceGeometryMethod;
            SourceGeometrySpecification = sourceGeometrySpecification;
            SourceExtendedWkbGeometry = sourceExtendedWkbGeometry;
            SourceIsOfficiallyAssigned = sourceIsOfficiallyAssigned;
            DestinationParentAddressPersistentLocalId = destinationParentAddressPersistentLocalId;
        }
    }
}
