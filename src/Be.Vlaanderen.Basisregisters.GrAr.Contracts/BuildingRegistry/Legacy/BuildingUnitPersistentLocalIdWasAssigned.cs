namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;
    using NodaTime;

    public class BuildingUnitPersistentLocalIdWasAssigned : IQueueMessage
    {
        public Guid BuildingId { get; }

        public Guid BuildingUnitId { get; }

        public int PersistentLocalId { get; }

        public Instant AssignmentDate { get; }

        public Provenance Provenance { get; }

        public BuildingUnitPersistentLocalIdWasAssigned(Guid buildingId,
            Guid buildingUnitId,
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
