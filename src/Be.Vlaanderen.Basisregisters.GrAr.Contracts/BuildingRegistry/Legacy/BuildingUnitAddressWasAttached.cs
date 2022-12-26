namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public class BuildingUnitAddressWasAttached : IQueueMessage
    {
        public string BuildingId { get; }

        public string AddressId { get; }

        public string To { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasAttached(
            string buildingId,
            string addressId,
            string to,
            Provenance provenance)
        {
            BuildingId = buildingId;
            AddressId = addressId;
            To = to;
            Provenance = provenance;
        }
    }
}
