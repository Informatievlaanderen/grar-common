namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class BuildingUnitWasMovedIntoBuilding : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }
        public int SourceBuildingPersistentLocalId { get; }
        public int BuildingUnitPersistentLocalId { get; }
        public string BuildingUnitStatus { get; }
        public string GeometryMethod { get; }
        public string ExtendedWkbGeometry { get; }
        public string Function { get; }
        public bool HasDeviation { get; }
        public List<int> AddressPersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasMovedIntoBuilding(
            int buildingPersistentLocalId,
            int sourceBuildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            string buildingUnitStatus,
            string geometryMethod,
            string extendedWkbGeometry,
            string function,
            bool hasDeviation,
            List<int> addressPersistentLocalIds,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            SourceBuildingPersistentLocalId = sourceBuildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            BuildingUnitStatus = buildingUnitStatus;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Function = function;
            HasDeviation = hasDeviation;
            AddressPersistentLocalIds = addressPersistentLocalIds;
            Provenance = provenance;
        }
    }
}
