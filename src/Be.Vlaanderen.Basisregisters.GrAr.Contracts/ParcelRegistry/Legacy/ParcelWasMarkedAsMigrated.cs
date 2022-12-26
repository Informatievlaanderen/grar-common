namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using System;
    using Contracts;
    using Common;

    public class ParcelWasMarkedAsMigrated : IQueueMessage
    {
        public Guid ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasMarkedAsMigrated(
            Guid parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
