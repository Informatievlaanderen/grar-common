namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public class BuildingUnitWasRealized : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasRealized(string buildingId,
            string buildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            Provenance = provenance;
        }
    }
}
