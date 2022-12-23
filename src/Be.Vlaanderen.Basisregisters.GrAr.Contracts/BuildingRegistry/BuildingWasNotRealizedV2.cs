namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingWasNotRealizedV2 : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingWasNotRealizedV2(int buildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
