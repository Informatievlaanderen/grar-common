namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public class BuildingUnitWasRetiredByParent : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string ParentBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasRetiredByParent(string buildingId,
            string buildingUnitId,
            string parentBuildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            ParentBuildingUnitId = parentBuildingUnitId;
            Provenance = provenance;
        }
    }
}
