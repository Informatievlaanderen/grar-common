namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Common;

    public sealed class ParcelWasRetiredV2 : IQueueMessage
    {
        public string ParcelId { get; }

        public string CaPaKey { get; }

        public Provenance Provenance { get; }

        public ParcelWasRetiredV2(
            string parcelId,
            string caPaKey,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            Provenance = provenance;
        }
    }
}
