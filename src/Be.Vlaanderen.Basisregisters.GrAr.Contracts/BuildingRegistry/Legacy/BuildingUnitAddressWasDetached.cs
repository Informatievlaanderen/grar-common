namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public class BuildingUnitAddressWasDetached : IQueueMessage
    {
        public Guid BuildingId { get; }

        public List<Guid> AddressIds { get; }

        public Guid From { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasDetached(Guid buildingId,
            IEnumerable<Guid> addressIds,
            Guid @from,
            Provenance provenance)
        {
            BuildingId = buildingId;
            AddressIds = addressIds.ToList();
            From = @from;
            Provenance = provenance;
        }
    }
}
