namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;
    using NodaTime;

    public sealed class BuildingUnitWasReaddedByOtherUnitRemoval : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public string AddressId { get; }

        public Instant BuildingUnitVersion { get; }

        public string PredecessorBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasReaddedByOtherUnitRemoval(string buildingId,
            string buildingUnitId,
            string buildingUnitKey,
            string addressId,
            Instant buildingUnitVersion,
            string predecessorBuildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            BuildingUnitKey = buildingUnitKey;
            AddressId = addressId;
            BuildingUnitVersion = buildingUnitVersion;
            PredecessorBuildingUnitId = predecessorBuildingUnitId;
            Provenance = provenance;
        }
    }
}
