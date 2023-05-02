namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class BuildingMeasurementWasChanged : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public List<int> BuildingUnitPersistentLocalIds { get; }

        public List<int> BuildingUnitPersistentLocalIdsWhichBecameDerived { get; }

        public string ExtendedWkbGeometryBuilding { get; }

        public string? ExtendedWkbGeometryBuildingUnits { get; }

        public Provenance Provenance { get; }

        public BuildingMeasurementWasChanged(int buildingPersistentLocalId,
            IEnumerable<int> buildingUnitPersistentLocalIds,
            IEnumerable<int> buildingUnitPersistentLocalIdsWhichBecameDerived,
            string extendedWkbGeometryBuilding,
            string? extendedWkbGeometryBuildingUnits,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalIds = buildingUnitPersistentLocalIds.ToList();
            BuildingUnitPersistentLocalIdsWhichBecameDerived = buildingUnitPersistentLocalIdsWhichBecameDerived.ToList();
            ExtendedWkbGeometryBuilding = extendedWkbGeometryBuilding;
            ExtendedWkbGeometryBuildingUnits = extendedWkbGeometryBuildingUnits;
            Provenance = provenance;
        }
    }
}
