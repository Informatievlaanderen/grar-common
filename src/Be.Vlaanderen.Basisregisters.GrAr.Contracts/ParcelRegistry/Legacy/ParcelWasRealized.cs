namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using Contracts;
    using Common;

    public class ParcelWasRealized : IQueueMessage
    {
        public string ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRealized(
            string parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
