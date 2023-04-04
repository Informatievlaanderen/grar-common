namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using Common;

    public class AddressWasReaddressed : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public IEnumerable<int> ProposedAddressPersistentLocalIds { get; }

        public IEnumerable<int> RejectedAddressPersistentLocalIds { get; }

        public IEnumerable<int> RetiredAddressPersistentLocalIds { get; }

        public IEnumerable<int> AddressesWhichWillBeRejectedOrRetiredPersistentLocalIds { get; }

        public IEnumerable<ReaddressedAddressData> ReaddressedAddresses { get; }

        public Provenance Provenance { get; }

        public AddressWasReaddressed(int streetNamePersistentLocalId,
            IEnumerable<int> proposedAddressPersistentLocalIds,
            IEnumerable<int> rejectedAddressPersistentLocalIds,
            IEnumerable<int> retiredAddressPersistentLocalIds,
            IEnumerable<int> addressesWhichWillBeRejectedOrRetiredPersistentLocalIds,
            IEnumerable<ReaddressedAddressData> readdressedAddresses,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            ProposedAddressPersistentLocalIds = proposedAddressPersistentLocalIds;
            RejectedAddressPersistentLocalIds = rejectedAddressPersistentLocalIds;
            RetiredAddressPersistentLocalIds = retiredAddressPersistentLocalIds;
            AddressesWhichWillBeRejectedOrRetiredPersistentLocalIds = addressesWhichWillBeRejectedOrRetiredPersistentLocalIds;
            ReaddressedAddresses = readdressedAddresses;
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
