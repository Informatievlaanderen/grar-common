namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitPositionCrsWasChanged : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public string GeometryMethod { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPositionCrsWasChanged(
            int buildingPersistentLocalId,
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
