namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasCorrectedFromRetiredToRealized : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasCorrectedFromRetiredToRealized(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            Provenance = provenance;
        }
    }
}
