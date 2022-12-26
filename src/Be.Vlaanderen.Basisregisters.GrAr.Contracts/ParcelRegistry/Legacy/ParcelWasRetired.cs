namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using System;
    using Contracts;
    using Common;

    public class ParcelWasRetired : IQueueMessage
    {
        public Guid ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRetired(
            Guid parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
