namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Common;

    public class BuildingWasCorrectedToRetired : IQueueMessage
    {
        public Guid BuildingId { get; }
        public List<Guid> BuildingUnitIdsToRetire { get; }
        public List<Guid> BuildingUnitIdsToNotRealize { get; }
        public Provenance Provenance { get; }

        public BuildingWasCorrectedToRetired(
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
