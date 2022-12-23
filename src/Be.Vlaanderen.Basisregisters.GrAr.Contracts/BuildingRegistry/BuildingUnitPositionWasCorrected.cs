namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitPositionWasCorrected : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPositionWasCorrected(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            string geometryMethod,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            GeometryMethod = geometryMethod;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
