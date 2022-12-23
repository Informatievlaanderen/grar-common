namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitAddressWasDetachedBecauseAddressWasRejected : IQueueMessage
    {
        public int BuildingPersistentLocalId { get; }
       
        public int BuildingUnitPersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public BuildingUnitAddressWasDetachedBecauseAddressWasRejected(int buildingPersistentLocalId,
            int buildingUnitPersistentLocalId,
            int addressPersistentLocalId,
            Provenance provenance)
        {
            BuildingPersistentLocalId = buildingPersistentLocalId;
            BuildingUnitPersistentLocalId = buildingUnitPersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
