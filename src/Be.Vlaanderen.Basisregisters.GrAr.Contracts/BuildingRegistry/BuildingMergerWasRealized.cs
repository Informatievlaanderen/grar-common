namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class BuildingMergerWasRealized : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public string ExtendedWkbGeometry { get; }

        public List<int> MergedBuildingPersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public BuildingMergerWasRealized(
            int buildingPersistentLocalId,
            string extendedWkbGeometry,
            List<int> mergedBuildingPersistentLocalIds,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            MergedBuildingPersistentLocalIds = mergedBuildingPersistentLocalIds;
            Provenance = provenance;
        }
    }
}
