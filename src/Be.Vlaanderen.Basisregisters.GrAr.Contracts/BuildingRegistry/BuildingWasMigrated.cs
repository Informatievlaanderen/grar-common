namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class BuildingWasMigrated : IQueueMessage
    {
        public string BuildingId { get; }

        public int BuildingPersistentLocalId { get; }

        public string BuildingPersistentLocalIdAssignmentDate { get; }

        public string BuildingStatus { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public bool IsRemoved { get; }

        public List<BuildingUnit> BuildingUnits { get; }

        public Provenance Provenance { get; }

        public BuildingWasMigrated(
            string buildingId,
            int buildingPersistentLocalId,
            string buildingPersistentLocalIdAssignmentDate,
            string buildingStatus,
            string geometryMethod,
            string extendedWkbGeometry,
            bool isRemoved,
            IEnumerable<BuildingUnit> buildingUnits,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingPersistentLocalIdAssignmentDate = buildingPersistentLocalIdAssignmentDate;
            BuildingStatus = buildingStatus;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            IsRemoved = isRemoved;
            BuildingUnits = buildingUnits.ToList();
            Provenance = provenance;
        }

        public sealed class BuildingUnit
        {
            public string BuildingUnitId { get; }

            public int BuildingUnitPersistentLocalId { get; }

            public string Function { get; }

            public string Status { get; }

            public List<int> AddressPersistentLocalIds { get; }

            public string GeometryMethod { get; }

            public string ExtendedWkbGeometry { get; }

            public bool IsRemoved { get; }

            public BuildingUnit(
                string buildingUnitId,
                int buildingUnitPersistentLocalId,
                string function,
                string status,
                List<int> addressPersistentLocalIds,
                string geometryMethod,
                string extendedWkbGeometry,
                bool isRemoved)
            {
                BuildingUnitId = buildingUnitId;
                BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
                Function = function;
                Status = status;
                AddressPersistentLocalIds = addressPersistentLocalIds;
                GeometryMethod = geometryMethod;
                ExtendedWkbGeometry = extendedWkbGeometry;
                IsRemoved = isRemoved;
            }
        }
    }
}
