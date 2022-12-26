namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public class BuildingWasMarkedAsMigrated : IQueueMessage
    {
        public string BuildingId { get; }
        public int PersistentLocalId { get; }
        public Provenance Provenance { get; }

        public BuildingWasMarkedAsMigrated(
            string buildingId,
            int persistentLocalId,
            Provenance provenance)
        {
            BuildingId = buildingId;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;
        }
    }
}
