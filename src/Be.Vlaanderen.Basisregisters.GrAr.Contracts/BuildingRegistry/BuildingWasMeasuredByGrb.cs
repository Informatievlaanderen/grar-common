namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingWasMeasuredByGrb : IQueueMessage
    {
        public Guid BuildingId { get; }
        public string ExtendedWkbGeometry { get; }
        public Provenance Provenance { get; }

        public BuildingWasMeasuredByGrb(
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
