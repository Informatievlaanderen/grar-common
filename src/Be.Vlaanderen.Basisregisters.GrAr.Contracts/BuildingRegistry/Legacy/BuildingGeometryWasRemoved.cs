namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingGeometryWasRemoved : IQueueMessage
    {
        public Guid BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingGeometryWasRemoved(
            Guid buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
