namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using Common;

    public class AddressHouseNumberWasReplacedBecauseOfReaddress : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int DestinationStreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public int DestinationAddressPersistentLocalId { get; }

        public IList<AddressBoxNumberReplacedBecauseOfReaddressData> BoxNumberAddressPersistentLocalIds { get;  }

        public Provenance Provenance { get; }

        public AddressHouseNumberWasReplacedBecauseOfReaddress(
            int streetNamePersistentLocalId,
            int destinationStreetNamePersistentLocalId,
            int addressPersistentLocalId,
            int destinationAddressPersistentLocalId,
            IList<AddressBoxNumberReplacedBecauseOfReaddressData> boxNumberAddressPersistentLocalIds,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            DestinationStreetNamePersistentLocalId = destinationStreetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
            BoxNumberAddressPersistentLocalIds = boxNumberAddressPersistentLocalIds;
            Provenance = provenance;
        }
    }

    public class AddressBoxNumberReplacedBecauseOfReaddressData
    {
        public int SourceAddressPersistentLocalId { get; }
        public int DestinationAddressPersistentLocalId { get; }

        public AddressBoxNumberReplacedBecauseOfReaddressData(
            int sourceAddressPersistentLocalId,
            int destinationAddressPersistentLocalId)
        {
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
        }
    }
}
