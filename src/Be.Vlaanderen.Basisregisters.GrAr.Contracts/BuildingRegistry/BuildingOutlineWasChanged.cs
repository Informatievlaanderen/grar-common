namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class BuildingOutlineWasChanged : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public IEnumerable<int> BuildingUnitPersistentLocalIds { get; }

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
            BuildingUnitPersistentLocalIds = buildingUnitPersistentLocalIds;
            ExtendedWkbGeometryBuilding = extendedWkbGeometryBuilding;
            ExtendedWkbGeometryBuildingUnits = extendedWkbGeometryBuildingUnits;
            Provenance = provenance;
        }
    }
}
