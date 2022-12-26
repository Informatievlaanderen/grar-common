namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingWasOutlined : IQueueMessage
    {
        public string BuildingId { get; }
        public string ExtendedWkbGeometry { get; }
        public Provenance Provenance { get; }

        public BuildingWasOutlined(
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
