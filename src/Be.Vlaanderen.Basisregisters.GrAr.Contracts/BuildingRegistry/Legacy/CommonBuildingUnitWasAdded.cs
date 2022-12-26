namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;
    using NodaTime;

    public class CommonBuildingUnitWasAdded : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public Instant BuildingUnitVersion { get; }

        public Provenance Provenance { get; }

        public CommonBuildingUnitWasAdded(string buildingId,
            string buildingUnitId,
            string buildingUnitKey,
            Instant buildingUnitVersion,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            BuildingUnitKey = buildingUnitKey;
            BuildingUnitVersion = buildingUnitVersion;
            Provenance = provenance;
        }
    }
}
