namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNamePersistentLocalIdWasAssigned : IQueueMessage
    {
        public string StreetNameId { get; }

        public int PersistentLocalId { get; }

        public string AssignmentDate { get; }

        public Provenance Provenance { get; }

        public StreetNamePersistentLocalIdWasAssigned(string streetNameId,
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
