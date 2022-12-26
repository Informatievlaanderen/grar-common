namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Contracts;
    using Common;

    public sealed class ParcelWasRegistered : IQueueMessage
    {
        public string ParcelId { get; }
        public string VbrCaPaKey { get; }
        public Provenance Provenance { get; }

        public ParcelWasRegistered(
            string parcelId,
            string vbrCaPaKey,
            Provenance provenance)
        {
            ParcelId = parcelId;
            VbrCaPaKey = vbrCaPaKey;
            Provenance = provenance;
        }
    }
}
