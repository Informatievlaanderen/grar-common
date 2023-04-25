namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class UnplannedBuildingWasRealizedAndMeasured : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public UnplannedBuildingWasRealizedAndMeasured(
            int buildingPersistentLocalId,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
