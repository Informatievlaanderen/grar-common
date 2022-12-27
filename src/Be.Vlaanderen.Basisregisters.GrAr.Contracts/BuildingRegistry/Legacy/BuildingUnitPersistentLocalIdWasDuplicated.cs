namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using Common;

    public sealed class BuildingUnitPersistentLocalIdWasDuplicated : IQueueMessage
    {
        public string BuildingId { get; }

        public string BuildingUnitId { get; }

        public int DuplicatePersistentLocalId { get; }

        public int OriginalPersistentLocalId { get; }

        public string DuplicateAssignmentDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPersistentLocalIdWasDuplicated(string buildingId,
            string buildingUnitId,
            int duplicatePersistentLocalId,
            int originalPersistentLocalId,
            string duplicateAssignmentDate,
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
