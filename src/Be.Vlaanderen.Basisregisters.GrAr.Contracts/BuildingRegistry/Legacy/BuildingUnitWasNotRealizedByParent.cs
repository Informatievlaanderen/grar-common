namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;

    public class BuildingUnitWasNotRealizedByParent : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public Guid ParentBuildingUnitId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitWasNotRealizedByParent(Guid buildingId,
            Guid buildingUnitId,
            Guid parentBuildingUnitId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            ParentBuildingUnitId = parentBuildingUnitId;
            Provenance = provenance;
        }
    }
}
