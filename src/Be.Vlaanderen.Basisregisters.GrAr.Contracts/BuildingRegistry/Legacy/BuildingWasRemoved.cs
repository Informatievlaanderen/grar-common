namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Common;

    public sealed class BuildingWasRemoved : IQueueMessage
    {
        public string BuildingId { get; }
        public List<string> BuildingUnitIds { get; }
        public Provenance Provenance { get; }

        public BuildingWasRemoved(
            string buildingId,
            IEnumerable<string> buildingUnitIds,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitIds = buildingUnitIds.ToList();
            Provenance = provenance;
        }
    }
}
