namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;

    public class BuildingUnitAddressWasAttached : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid AddressId { get; }

        public Guid To { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasAttached(
            Guid buildingId,
            Guid addressId,
            Guid to,
            Provenance provenance)
        {
            BuildingId = buildingId;
            AddressId = addressId;
            To = to;
            Provenance = provenance;
        }
    }
}
