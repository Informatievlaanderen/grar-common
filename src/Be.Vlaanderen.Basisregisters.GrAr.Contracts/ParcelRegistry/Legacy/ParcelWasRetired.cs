namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using Contracts;
    using Common;

    public class ParcelWasRetired : IQueueMessage
    {
        public string ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRetired(
            string parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
