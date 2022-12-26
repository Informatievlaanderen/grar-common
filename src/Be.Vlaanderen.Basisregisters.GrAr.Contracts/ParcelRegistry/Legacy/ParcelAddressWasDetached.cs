namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using System;
    using Contracts;
    using Common;

    public class ParcelAddressWasDetached : IQueueMessage
    {
        public Guid ParcelId { get; }
        public Guid AddressId { get; }
        public Provenance Provenance { get; }

        public ParcelAddressWasDetached(
            Guid parcelId,
            Guid addressId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
