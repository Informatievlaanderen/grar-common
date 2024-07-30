namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasRejectedBecauseOfMunicipalityMerger : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int? NewAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public AddressWasRejectedBecauseOfMunicipalityMerger(
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
