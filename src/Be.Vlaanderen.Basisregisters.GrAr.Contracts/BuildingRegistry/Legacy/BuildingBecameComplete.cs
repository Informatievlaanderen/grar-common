namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingBecameComplete : IQueueMessage
    {
        public string BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingBecameComplete(
            string buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
