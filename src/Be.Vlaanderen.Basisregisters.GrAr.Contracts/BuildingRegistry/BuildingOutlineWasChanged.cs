namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class BuildingOutlineWasChanged : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public List<int> BuildingUnitPersistentLocalIds { get; }

        public string ExtendedWkbGeometryBuilding { get; }

        public string? ExtendedWkbGeometryBuildingUnits { get; }

        public Provenance Provenance { get; }

        public BuildingOutlineWasChanged(int buildingPersistentLocalId,
            IEnumerable<int> buildingUnitPersistentLocalIds,
            string extendedWkbGeometryBuilding,
            string? extendedWkbGeometryBuildingUnits,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalIds = buildingUnitPersistentLocalIds.ToList();
            ExtendedWkbGeometryBuilding = extendedWkbGeometryBuilding;
            ExtendedWkbGeometryBuildingUnits = extendedWkbGeometryBuildingUnits;
            Provenance = provenance;
        }
    }
}
