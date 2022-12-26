namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;
    using NodaTime;

    public class CommonBuildingUnitWasAdded : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public string BuildingUnitKey { get; }

        public Instant BuildingUnitVersion { get; }

        public Provenance Provenance { get; }

        public CommonBuildingUnitWasAdded(Guid buildingId,
            Guid buildingUnitId,
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
