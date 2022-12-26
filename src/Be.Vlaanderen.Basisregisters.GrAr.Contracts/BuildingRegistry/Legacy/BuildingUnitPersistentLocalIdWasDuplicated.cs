namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;
    using NodaTime;

    public class BuildingUnitPersistentLocalIdWasDuplicated : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public int DuplicatePersistentLocalId { get; }

        public int OriginalPersistentLocalId { get; }

        public Instant DuplicateAssignmentDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPersistentLocalIdWasDuplicated(string buildingId,
            string buildingUnitId,
            int duplicatePersistentLocalId,
            int originalPersistentLocalId,
            Instant duplicateAssignmentDate,
            Provenance provenance)
        {
            BuildingId = buildingId;
            BuildingUnitId = buildingUnitId;
            DuplicatePersistentLocalId = duplicatePersistentLocalId;
            OriginalPersistentLocalId = originalPersistentLocalId;
            DuplicateAssignmentDate = duplicateAssignmentDate;
            Provenance = provenance;
        }
    }
}
