namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.ParcelRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public sealed class ParcelAddressesWereReaddressed : IQueueMessage
    {
        public string ParcelId { get; }

        public string CaPaKey { get; }

        public List<int> AttachedAddressPersistentLocalIds { get; }

        public List<int> DetachedAddressPersistentLocalIds { get; }

        public List<AddressRegistryReaddress> AddressRegistryReaddresses { get; }

        public Provenance Provenance { get; }

        public ParcelAddressesWereReaddressed(string parcelId,
            string caPaKey,
            IEnumerable<int> attachedAddressPersistentLocalIds,
            IEnumerable<int> detachedAddressPersistentLocalIds,
            IEnumerable<AddressRegistryReaddress> addressRegistryReaddresses,
            Provenance provenance)
        {
            ParcelId = parcelId;
            CaPaKey = caPaKey;
            AttachedAddressPersistentLocalIds = attachedAddressPersistentLocalIds.ToList();
            DetachedAddressPersistentLocalIds = detachedAddressPersistentLocalIds.ToList();
            AddressRegistryReaddresses = addressRegistryReaddresses.ToList();
            Provenance = provenance;
        }
    }

    public sealed class AddressRegistryReaddress
    {
        public int SourceAddressPersistentLocalId { get; }

        public int DestinationAddressPersistentLocalId { get; }

        public AddressRegistryReaddress(int sourceAddressPersistentLocalId,
            int destinationAddressPersistentLocalId)
        {
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
        }
    }
}
