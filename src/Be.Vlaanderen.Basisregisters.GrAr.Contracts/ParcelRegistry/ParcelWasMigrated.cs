namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System;
    using System.Collections.Generic;
    using Common;

    public sealed class ParcelWasMigrated : IQueueMessage
    {
        public Guid OldParcelId { get; }

        public Guid ParcelId { get; }

        public string CaPaKey { get; }

        public string ParcelStatus { get; }

        public bool IsRemoved { get; }

        public List<int> AddressPersistentLocalIds { get; }

        public decimal? XCoordinate { get; }

        public decimal? YCoordinate { get; }

        public Provenance Provenance { get; }

        public ParcelWasMigrated(
            Guid oldParcelId,
            Guid parcelId,
            string caPaKey,
            string parcelStatus,
            bool isRemoved,
            List<int> addressPersistentLocalIds,
            decimal? xCoordinate,
            decimal? yCoordinate,
            Provenance provenance)
        {
            OldParcelId = oldParcelId;
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            ParcelStatus = parcelStatus;
            IsRemoved = isRemoved;
            AddressPersistentLocalIds = addressPersistentLocalIds;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Provenance = provenance;
        }
    }
}
