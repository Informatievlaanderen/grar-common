namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNamePersistentLocalIdWasAssigned : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public int PersistentLocalId { get; }

        public string AssignmentDate { get; }

        public Provenance Provenance { get; }

        public StreetNamePersistentLocalIdWasAssigned(Guid streetNameId,
            int persistentLocalId,
            string assignmentDate,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            PersistentLocalId = persistentLocalId;
            AssignmentDate = assignmentDate;
            Provenance = provenance;    
        }
    }
}
