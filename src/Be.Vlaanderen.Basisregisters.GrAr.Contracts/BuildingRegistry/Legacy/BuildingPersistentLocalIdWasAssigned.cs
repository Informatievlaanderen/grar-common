namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Contracts;
    using Common;

    public sealed class BuildingPersistentLocalIdWasAssigned : IQueueMessage
    {
        public string BuildingId { get; }
        public int PersistentLocalId { get; }
        public string AssignmentDate { get; }
        public Provenance Provenance { get; }

        public BuildingPersistentLocalIdWasAssigned(
            string buildingId,
            int persistentLocalId,
            string assignmentDate,
            Provenance provenance)
        {
            BuildingId = buildingId;
            PersistentLocalId = persistentLocalId;
            AssignmentDate = assignmentDate;
            Provenance = provenance;
        }
    }
}
