namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingWasCorrectedToPlanned : IQueueMessage
    {
        public string BuildingId { get; }
        public Provenance Provenance { get; }

        public BuildingWasCorrectedToPlanned(
            string buildingId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            Provenance = provenance;
        }
    }
}
