namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasRealizedV2 : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }
        
        public Provenance Provenance { get; }

        public BuildingUnitWasRealizedV2(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            Provenance = provenance;
        }
    }
}
