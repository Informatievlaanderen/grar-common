namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressPersistentLocalIdWasAssigned : IQueueMessage
    {
        public string AddressId { get; }

        public int PersistentLocalId { get; }

        public string AssignmentDate { get; }

        public Provenance Provenance { get; }

        public AddressPersistentLocalIdWasAssigned(string addressId,
            int persistentLocalId,
            string assignmentDate,
            Provenance provenance)
        {
            AddressId = addressId;
            PersistentLocalId = persistentLocalId;
            AssignmentDate = assignmentDate;
            Provenance = provenance;
        }
    }
}
