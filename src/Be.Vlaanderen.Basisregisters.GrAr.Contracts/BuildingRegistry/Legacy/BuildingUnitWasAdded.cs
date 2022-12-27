namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasAdded : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public string AddressId { get; }

        public string BuildingUnitVersion { get; }

        public string? PredecessorBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasAdded(string buildingId,
            string buildingUnitId,
            string buildingUnitKey,
            string addressId,
            string buildingUnitVersion,
            string? predecessorBuildingUnitId,
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
