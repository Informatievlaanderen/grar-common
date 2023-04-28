namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class BuildingWasMeasured : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public List<int> BuildingUnitPersistentLocalIds { get; }

        public List<int> BuildingUnitPersistentLocalIdsWhichBecameDerived { get; }

        public string ExtendedWkbGeometryBuilding { get; }

        public string? ExtendedWkbGeometryBuildingUnits { get; }

        public Provenance Provenance { get; }

        public BuildingWasMeasured(
            int buildingPersistentLocalId,
            List<int> buildingUnitPersistentLocalIds,
            List<int> buildingUnitPersistentLocalIdsWhichBecameDerived,
            string extendedWkbGeometryBuilding,
            string? extendedWkbGeometryBuildingUnits,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalIds = buildingUnitPersistentLocalIds;
            BuildingUnitPersistentLocalIdsWhichBecameDerived = buildingUnitPersistentLocalIdsWhichBecameDerived;
            ExtendedWkbGeometryBuilding = extendedWkbGeometryBuilding;
            ExtendedWkbGeometryBuildingUnits = extendedWkbGeometryBuildingUnits;
            Provenance = provenance;
        }
    }
}
