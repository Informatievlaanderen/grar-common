namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingWasPlanned : IQueueMessage
    {
        public Guid BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingWasPlanned(
            Guid buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
