namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRetiredBecauseOfMunicipalityMerger : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int? NewAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public AddressWasRetiredBecauseOfMunicipalityMerger(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            int? newAddressPersistentLocalId,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            NewAddressPersistentLocalId = newAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
