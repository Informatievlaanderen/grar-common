namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class BuildingUnitAddressWasDetached : IQueueMessage
    {
        public string BuildingId { get; }

        public List<string> AddressIds { get; }

        public string From { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasDetached(string buildingId,
            IEnumerable<string> addressIds,
            string @from,
            Provenance provenance)
        {
            BuildingId = buildingId;
            AddressIds = addressIds.ToList();
            From = @from;
            Provenance = provenance;
        }
    }
}
