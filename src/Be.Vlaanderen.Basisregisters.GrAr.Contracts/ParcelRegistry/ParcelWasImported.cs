namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Common;

    public sealed class ParcelWasImported : IQueueMessage
    {
        public string ParcelId { get; }

        public string CaPaKey { get; }

        public string ExtendedWkbGeometry { get; }

        public Provenance Provenance { get; }

        public ParcelWasImported(
            string parcelId,
            string caPaKey,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
