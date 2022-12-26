namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;
    using NodaTime;

    public class BuildingUnitWasAddedToRetiredBuilding : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public Guid AddressId { get; }

        public Instant BuildingUnitVersion { get; }

        public Guid? PredecessorBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasAddedToRetiredBuilding(Guid buildingId,
            Guid buildingUnitId,
            string buildingUnitKey,
            Guid addressId,
            Instant buildingUnitVersion,
            Guid? predecessorBuildingUnitId,
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
