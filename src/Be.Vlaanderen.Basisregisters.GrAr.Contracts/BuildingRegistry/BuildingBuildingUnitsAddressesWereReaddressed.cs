namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class BuildingBuildingUnitsAddressesWereReaddressed : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public List<BuildingUnitAddressesWereReaddressed> BuildingUnitsReaddresses { get; }

        public List<AddressRegistryReaddress> AddressRegistryReaddresses { get; }

        public Provenance Provenance { get; }

        public BuildingBuildingUnitsAddressesWereReaddressed(
            int buildingPersistentLocalId,
            IEnumerable<BuildingUnitAddressesWereReaddressed> buildingUnitsReaddresses,
            IEnumerable<AddressRegistryReaddress> addressRegistryReaddresses,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitsReaddresses = buildingUnitsReaddresses.ToList();
            AddressRegistryReaddresses = addressRegistryReaddresses.ToList();
            Provenance = provenance;
        }
    }

    public sealed class BuildingUnitAddressesWereReaddressed
    {
        public int BuildingUnitPersistentLocalId { get; }

        public List<int> AttachedAddressPersistentLocalIds { get; }

        public List<int> DetachedAddressPersistentLocalIds { get; }

        public BuildingUnitAddressesWereReaddressed(
            int buildingUnitPersistentLocalId,
            IEnumerable<int> attachedAddressPersistentLocalIds,
            IEnumerable<int> detachedAddressPersistentLocalIds)
        {
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            AttachedAddressPersistentLocalIds = attachedAddressPersistentLocalIds.ToList();
            DetachedAddressPersistentLocalIds = detachedAddressPersistentLocalIds.ToList();
        }
    }
}
