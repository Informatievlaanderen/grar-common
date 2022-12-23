namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingBecameUnderConstructionV2 : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingBecameUnderConstructionV2(int buildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
