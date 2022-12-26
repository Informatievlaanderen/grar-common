namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using Contracts;
    using Common;

    public class ParcelWasRealized : IQueueMessage
    {
        public Guid ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRealized(
            Guid parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
