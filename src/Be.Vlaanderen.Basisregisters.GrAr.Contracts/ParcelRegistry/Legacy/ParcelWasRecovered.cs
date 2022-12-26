namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry.Legacy
{
    using System;
    using Contracts;
    using Common;

    public class ParcelWasRecovered : IQueueMessage
    {
        public Guid ParcelId { get; }
        public Provenance Provenance { get; }

        public ParcelWasRecovered(
            Guid parcelId,
            Provenance provenance)
        {
            ParcelId = parcelId;
            Provenance = provenance;
        }
    }
}
