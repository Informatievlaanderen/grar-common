namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public class BuildingUnitAddressWasReplacedBecauseAddressWasReaddressed : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }

        public int BuildingUnitPersistentLocalId { get; }

        public int NewAddressPersistentLocalId { get; }

        public int PreviousAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasReplacedBecauseAddressWasReaddressed(
            int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            int newAddressPersistentLocalId,
            int previousAddressPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            NewAddressPersistentLocalId = newAddressPersistentLocalId;
            PreviousAddressPersistentLocalId = previousAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
