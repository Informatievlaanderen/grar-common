namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using Contracts;
    using Common;

    public class ParcelAddressWasDetached : IQueueMessage
    {
        public string ParcelId { get; }
        public string AddressId { get; }
        public Provenance Provenance { get; }

        public ParcelAddressWasDetached(
            string parcelId,
            string addressId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            AddressId = addressId;
            Provenance = provenance;
        }
    }
}
