namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using Contracts;
    using Common;

    public class ParcelWasMarkedAsMigrated : IQueueMessage
    {
        public string ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasMarkedAsMigrated(
            string parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
