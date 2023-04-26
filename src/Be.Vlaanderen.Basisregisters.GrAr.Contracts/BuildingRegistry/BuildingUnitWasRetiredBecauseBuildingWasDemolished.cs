namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasRetiredBecauseBuildingWasDemolished : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }
        public int BuildingUnitPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasRetiredBecauseBuildingWasDemolished(
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
