namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasMoved : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public int DestinationBuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasMoved(
            int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            int destinationBuildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            DestinationBuildingPersistentLocalId = destinationBuildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
