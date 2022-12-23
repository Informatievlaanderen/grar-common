namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.BuildingRegistry
{
    using System;
    using Contracts;
    using Common;

    public class BuildingPersistentLocalIdWasAssigned : IQueueMessage
    {
        public Guid BuildingId { get; }
        public int PersistentLocalId { get; }
        public string AssignmentDate { get; }
        public Provenance Provenance { get; }

        public BuildingPersistentLocalIdWasAssigned(
            Guid buildingId,
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
