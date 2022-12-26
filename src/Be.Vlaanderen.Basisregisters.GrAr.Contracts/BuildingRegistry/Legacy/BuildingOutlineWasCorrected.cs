namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry.Legacy
{
    using Contracts;
    using Common;

    public class BuildingOutlineWasCorrected : IQueueMessage
    {
        public string BuildingId { get; }
        public string ExtendedWkbGeometry { get; }
        public Provenance Provenance { get; }

        public BuildingOutlineWasCorrected(
            string buildingId,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            BuildingId = buildingId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
