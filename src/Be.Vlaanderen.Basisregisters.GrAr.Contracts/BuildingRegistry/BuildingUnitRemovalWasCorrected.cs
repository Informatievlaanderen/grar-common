namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitRemovalWasCorrected : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public string BuildingUnitStatus { get; }

        public string Function { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public bool HasDeviation { get; }

        public Provenance Provenance { get; }

        public BuildingUnitRemovalWasCorrected(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            string buildingUnitStatus,
            string function,
            string geometryMethod,
            string extendedWkbGeometry,
            bool hasDeviation,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            BuildingUnitStatus = buildingUnitStatus;
            Function = function;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            HasDeviation = hasDeviation;
            Provenance = provenance;
        }
    }
}
