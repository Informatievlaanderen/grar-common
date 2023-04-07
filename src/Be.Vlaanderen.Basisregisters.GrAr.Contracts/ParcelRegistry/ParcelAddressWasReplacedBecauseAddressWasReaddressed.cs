namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Common;

    public sealed class ParcelAddressWasReplacedBecauseAddressWasReaddressed : IQueueMessage
    {
        public string ParcelId { get; }

        public string CaPaKey { get; }

        public int AddressPersistentLocalId { get; }

        public int PreviousAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public ParcelAddressWasReplacedBecauseAddressWasReaddressed(
            string parcelId,
            string caPaKey,
            int addressPersistentLocalId,
            int previousAddressPersistentLocalId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            AddressPersistentLocalId = addressPersistentLocalId;
            PreviousAddressPersistentLocalId = previousAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
