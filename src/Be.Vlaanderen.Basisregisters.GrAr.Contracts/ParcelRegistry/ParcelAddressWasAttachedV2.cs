namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using Common;

    public sealed class ParcelAddressWasAttachedV2 : IQueueMessage
    {
        public Guid ParcelId { get; }

        public int AddressPersistentLocalId { get; }

        public Provenance Provenance { get; }

        public ParcelAddressWasAttachedV2(
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
