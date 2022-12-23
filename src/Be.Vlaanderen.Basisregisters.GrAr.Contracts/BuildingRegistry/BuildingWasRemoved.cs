namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Common;

    public class BuildingWasRemoved : IQueueMessage
    {
        public Guid BuildingId { get; }
        public List<Guid> BuildingUnitIds { get; }
        public Provenance Provenance { get; }

        public BuildingWasRemoved(
            Guid buildingId,
            IEnumerable<Guid> buildingUnitIds,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitIds = buildingUnitIds.ToList();
            Provenance = provenance;
        }
    }
}
