namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class CommonBuildingUnitWasAdded : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public string BuildingUnitVersion { get; }

        public Provenance Provenance { get; }

        public CommonBuildingUnitWasAdded(string buildingId,
            string buildingUnitId,
            string buildingUnitKey,
            string buildingUnitVersion,
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
