namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Common;

    public class BuildingUnitPersistentLocalIdWasRemoved : IQueueMessage
    {
        public Guid BuildingId { get; set; }

        public string PersistentLocalId { get; set; }

        public string AssignmentDate { get; set; }

        public string Reason { get; set; }

        public Provenance Provenance { get; set; }

        public BuildingUnitPersistentLocalIdWasRemoved(Guid buildingId,
            string persistentLocalId,
            string assignmentDate,
            string reason,
            Provenance provenance)
        {
            BuildingId = buildingId;
            PersistentLocalId = persistentLocalId;
            AssignmentDate = assignmentDate;
            Reason = reason;
            Provenance = provenance;
        }
    }
}
