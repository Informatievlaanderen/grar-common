namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using Common;

    public sealed class ParcelAddressWasDetachedBecauseAddressWasRejected : IQueueMessage
    {
        public Guid ParcelId { get; }

        public string CaPaKey { get; }

        public int AddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public ParcelAddressWasDetachedBecauseAddressWasRejected(
            Guid parcelId,
            string caPaKey,
            int addressPersistentLocalId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            AddressPersistentLocalId = addressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
