namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class ParcelWasMigrated : IQueueMessage
    {
        public string OldParcelId { get; }

        public string ParcelId { get; }

        public string CaPaKey { get; }

        public string ParcelStatus { get; }

        public bool IsRemoved { get; }

        public List<int> AddressPersistentLocalIds { get; }

        public decimal? XCoordinate { get; }

        public decimal? YCoordinate { get; }

        public Provenance Provenance { get; }

        public ParcelWasMigrated(
            string oldParcelId,
            string parcelId,
            string caPaKey,
            string parcelStatus,
            bool isRemoved,
            IEnumerable<int> addressPersistentLocalIds,
            decimal? xCoordinate,
            decimal? yCoordinate,
            Provenance provenance)
        {
            OldParcelId = oldParcelId;
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            ParcelStatus = parcelStatus;
            IsRemoved = isRemoved;
            AddressPersistentLocalIds = addressPersistentLocalIds.ToList();
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Provenance = provenance;
        }
    }
}
