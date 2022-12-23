namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingWasCorrectedFromNotRealizedToPlanned : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingWasCorrectedFromNotRealizedToPlanned(int buildingPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            Provenance = provenance;
        }
    }
}
