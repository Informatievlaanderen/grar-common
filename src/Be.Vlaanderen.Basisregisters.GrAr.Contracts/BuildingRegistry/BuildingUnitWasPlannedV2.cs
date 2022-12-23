namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasPlannedV2 : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public string Function { get; }

        public bool HasDeviation { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasPlannedV2(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            string geometryMethod,
            string extendedWkbGeometry,
            string function,
            bool hasDeviation,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Function = function;
            HasDeviation = hasDeviation;
            Provenance = provenance;
        }
    }
}
