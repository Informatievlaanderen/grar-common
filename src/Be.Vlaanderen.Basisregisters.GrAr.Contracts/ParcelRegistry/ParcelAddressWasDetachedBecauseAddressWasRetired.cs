namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using Common;

    public sealed class ParcelAddressWasDetachedBecauseAddressWasRetired : IQueueMessage
    {
        public Guid ParcelId { get; }

        public int AddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public ParcelAddressWasDetachedBecauseAddressWasRetired(
            Guid parcelId,
            int addressPersistentLocalId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            AddressPersistentLocalId = addressPersistentLocalId;
            Provenance = provenance;
        }
    }
}
