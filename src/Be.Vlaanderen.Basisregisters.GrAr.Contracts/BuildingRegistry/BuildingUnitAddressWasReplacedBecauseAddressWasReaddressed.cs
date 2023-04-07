namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public class BuildingUnitAddressWasReplacedBecauseAddressWasReaddressed : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public int SourceAddressPersistentLocalId { get; }

        public int DestinationAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasReplacedBecauseAddressWasReaddressed(
            int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            int sourceAddressPersistentLocalId,
            int destinationAddressPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
