namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;
    using NodaTime;

    public sealed class BuildingUnitWasReaddressed : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string OldAddressId { get; }

        public string NewAddressId { get; }

        public LocalDate BeginDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasReaddressed(string buildingId,
            string buildingUnitId,
            string oldAddressId,
            string newAddressId,
            LocalDate beginDate,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            OldAddressId = oldAddressId;
            NewAddressId = newAddressId;
            BeginDate = beginDate;
            Provenance = provenance;
        }
    }
}
