namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingWasDemolished : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingWasDemolished(
            int buildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
