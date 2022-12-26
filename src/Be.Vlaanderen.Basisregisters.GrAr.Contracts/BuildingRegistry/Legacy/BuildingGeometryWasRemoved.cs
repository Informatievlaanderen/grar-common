namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingGeometryWasRemoved : IQueueMessage
    {
        public string BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingGeometryWasRemoved(
            string buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
