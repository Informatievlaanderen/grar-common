namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry.Legacy
{
    using System;
    using Contracts;
    using Common;

    public class BuildingOutlineWasCorrected : IQueueMessage
    {
        public Guid BuildingId { get; }
        public string ExtendedWkbGeometry { get; }
        public Provenance Provenance { get; }

        public BuildingOutlineWasCorrected(
            Guid buildingId,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            BuildingId = buildingId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
