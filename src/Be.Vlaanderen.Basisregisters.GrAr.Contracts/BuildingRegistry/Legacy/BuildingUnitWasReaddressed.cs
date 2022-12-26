namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;
    using NodaTime;

    public class BuildingUnitWasReaddressed : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public Guid OldAddressId { get; }

        public Guid NewAddressId { get; }

        public LocalDate BeginDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasReaddressed(Guid buildingId,
            Guid buildingUnitId,
            Guid oldAddressId,
            Guid newAddressId,
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
