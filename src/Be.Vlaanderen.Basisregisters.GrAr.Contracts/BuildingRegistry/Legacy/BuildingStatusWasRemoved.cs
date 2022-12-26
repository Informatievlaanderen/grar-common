namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingStatusWasRemoved : IQueueMessage
    {
        public string BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingStatusWasRemoved(
            string buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
