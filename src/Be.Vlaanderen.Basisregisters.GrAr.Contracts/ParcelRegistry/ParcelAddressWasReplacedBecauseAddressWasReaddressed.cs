namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Common;

    public sealed class ParcelAddressWasReplacedBecauseAddressWasReaddressed : IQueueMessage
    {
        public string ParcelId { get; }

        public string CaPaKey { get; }

        public int NewAddressPersistentLocalId { get; }

        public int PreviousAddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public ParcelAddressWasReplacedBecauseAddressWasReaddressed(
            string parcelId,
            string caPaKey,
            int newAddressPersistentLocalId,
            int previousAddressPersistentLocalId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            NewAddressPersistentLocalId = newAddressPersistentLocalId;
            PreviousAddressPersistentLocalId = previousAddressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
