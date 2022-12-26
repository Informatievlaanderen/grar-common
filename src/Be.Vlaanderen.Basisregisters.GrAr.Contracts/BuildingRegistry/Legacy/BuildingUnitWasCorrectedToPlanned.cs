namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasCorrectedToPlanned : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasCorrectedToPlanned(string buildingId,
            string buildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            Provenance = provenance;
        }
    }
}
