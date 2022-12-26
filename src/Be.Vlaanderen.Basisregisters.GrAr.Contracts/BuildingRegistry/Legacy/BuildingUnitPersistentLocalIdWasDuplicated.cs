namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;
    using NodaTime;

    public class BuildingUnitPersistentLocalIdWasDuplicated : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public int DuplicatePersistentLocalId { get; }

        public int OriginalPersistentLocalId { get; }

        public Instant DuplicateAssignmentDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPersistentLocalIdWasDuplicated(Guid buildingId,
            Guid buildingUnitId,
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
