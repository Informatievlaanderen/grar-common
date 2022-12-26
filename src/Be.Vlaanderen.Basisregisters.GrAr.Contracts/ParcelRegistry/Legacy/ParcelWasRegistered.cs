namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using Contracts;
    using Common;

    public class ParcelWasRegistered : IQueueMessage
    {
        public Guid ParcelId { get; }
        public string VbrCaPaKey { get; }
        public Provenance Provenance { get; }

        public ParcelWasRegistered(
            Guid parcelId,
            string vbrCaPaKey,
            Provenance provenance)
        {
            ParcelId = parcelId;
            VbrCaPaKey = vbrCaPaKey;
            Provenance = provenance;
        }
    }
}
