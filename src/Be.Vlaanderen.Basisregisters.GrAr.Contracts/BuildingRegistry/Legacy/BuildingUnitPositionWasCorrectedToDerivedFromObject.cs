namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitPositionWasCorrectedToDerivedFromObject : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPositionWasCorrectedToDerivedFromObject(string buildingId,
            string buildingUnitId,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
