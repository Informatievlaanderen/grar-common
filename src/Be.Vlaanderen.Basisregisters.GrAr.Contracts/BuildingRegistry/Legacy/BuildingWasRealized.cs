namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingWasRealized : IQueueMessage
    {
        public string BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingWasRealized(
            string buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
