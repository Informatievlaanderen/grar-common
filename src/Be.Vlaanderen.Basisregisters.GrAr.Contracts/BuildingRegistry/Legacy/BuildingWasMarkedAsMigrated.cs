namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingWasMarkedAsMigrated : IQueueMessage
    {
        public Guid BuildingId { get; }
        public int PersistentLocalId { get; }
        public Provenance Provenance { get; }

        public BuildingWasMarkedAsMigrated(
            Guid buildingId,
            int persistentLocalId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;
        }
    }
}
