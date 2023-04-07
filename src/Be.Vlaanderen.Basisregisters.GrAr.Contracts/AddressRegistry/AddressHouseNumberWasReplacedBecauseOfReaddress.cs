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
