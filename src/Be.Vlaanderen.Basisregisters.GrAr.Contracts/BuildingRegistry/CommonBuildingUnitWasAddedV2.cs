namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class CommonBuildingUnitWasAddedV2 : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public string BuildingUnitStatus { get; set; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public bool HasDeviation { get; }

        public Provenance Provenance { get; }

        public CommonBuildingUnitWasAddedV2(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            string buildingUnitStatus,
            string geometryMethod,
            string extendedWkbGeometry,
            bool hasDeviation,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            BuildingUnitStatus = buildingUnitStatus;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            HasDeviation = hasDeviation;
            Provenance = provenance;    
        }
    }
}
