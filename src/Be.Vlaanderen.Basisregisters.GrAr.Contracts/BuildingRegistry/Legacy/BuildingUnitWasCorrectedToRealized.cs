namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;

    public class BuildingUnitWasCorrectedToRealized : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasCorrectedToRealized(Guid buildingId,
            Guid buildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            Provenance = provenance;
        }
    }
}
