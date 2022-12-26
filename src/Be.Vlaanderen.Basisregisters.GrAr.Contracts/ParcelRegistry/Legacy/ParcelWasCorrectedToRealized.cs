namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using Contracts;
    using Common;

    public class ParcelWasCorrectedToRealized : IQueueMessage
    {
        public string ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasCorrectedToRealized(
            string parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
