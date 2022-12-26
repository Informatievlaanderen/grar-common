namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitWasNotRealizedByParent : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public string ParentBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasNotRealizedByParent(string buildingId,
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
