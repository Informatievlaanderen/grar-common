namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingWasCorrectedToRealized : IQueueMessage
    {
        public Guid BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingWasCorrectedToRealized(
            Guid buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
