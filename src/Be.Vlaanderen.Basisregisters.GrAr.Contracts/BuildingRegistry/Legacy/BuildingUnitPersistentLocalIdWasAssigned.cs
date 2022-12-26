namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;
    using NodaTime;

    public class BuildingUnitPersistentLocalIdWasAssigned : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public int PersistentLocalId { get; }

        public Instant AssignmentDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPersistentLocalIdWasAssigned(string buildingId,
            string buildingUnitId,
            int persistentLocalId,
            Instant assignmentDate,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            PersistentLocalId = persistentLocalId;
            AssignmentDate = assignmentDate;
            Provenance = provenance;
        }
    }
}
