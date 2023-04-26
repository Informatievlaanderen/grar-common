namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasNotRealizedBecauseBuildingWasDemolished : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }
        public int BuildingUnitPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasNotRealizedBecauseBuildingWasDemolished(
            int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            Provenance = provenance;
        }
    }
}
