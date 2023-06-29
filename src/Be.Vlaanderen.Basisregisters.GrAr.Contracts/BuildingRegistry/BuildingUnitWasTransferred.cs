namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class BuildingUnitWasTransferred : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public int SourceBuildingPersistentLocalId { get; }

        public string Function { get; }

        public string Status { get; }

        public List<int> AddressPersistentLocalIds { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public bool HasDeviation { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasTransferred(
            int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            int sourceBuildingPersistentLocalId,
            string function,
            string status,
            List<int> addressPersistentLocalIds,
            string geometryMethod,
            string extendedWkbGeometry,
            bool hasDeviation,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            SourceBuildingPersistentLocalId = sourceBuildingPersistentLocalId;
            Function = function;
            Status = status;
            AddressPersistentLocalIds = addressPersistentLocalIds;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            HasDeviation = hasDeviation;
            Provenance = provenance;
        }
    }
}
