namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using Contracts;
    using Common;

    public class ParcelWasRemoved : IQueueMessage
    {
        public string ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRemoved(
            string parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
