namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Common;

    public class BuildingWasRetired : IQueueMessage
    {
        public Guid BuildingId { get; }
        public List<Guid> BuildingUnitIdsToRetire { get; }
        public List<Guid> BuildingUnitIdsToNotRealize { get; }
        public Provenance Provenance { get; }

        public BuildingWasRetired(
            Guid buildingId,
            IEnumerable<Guid> buildingUnitIdsToRetire,
            IEnumerable<Guid> buildingUnitIdsToNotRealize,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitIdsToRetire = buildingUnitIdsToRetire.ToList();
            BuildingUnitIdsToNotRealize = buildingUnitIdsToNotRealize.ToList();
            Provenance = provenance;
        }
    }
}
