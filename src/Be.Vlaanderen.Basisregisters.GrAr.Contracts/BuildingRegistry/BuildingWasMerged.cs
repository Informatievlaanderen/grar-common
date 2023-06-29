namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingWasMerged : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int DestinationBuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingWasMerged(
            int buildingPersistentLocalId,
            int destinationBuildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            DestinationBuildingPersistentLocalId = destinationBuildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
