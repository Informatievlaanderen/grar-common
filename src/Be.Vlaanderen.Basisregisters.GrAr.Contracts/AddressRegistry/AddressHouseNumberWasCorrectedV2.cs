namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public class AddressHouseNumberWasCorrectedV2 : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public List<int> BoxNumberPersistentLocalIds { get; }

        public string HouseNumber { get; }

        public Provenance Provenance { get; }

        public AddressHouseNumberWasCorrectedV2(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            IEnumerable<int> boxNumberPersistentLocalIds,
            string houseNumber,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            BoxNumberPersistentLocalIds = boxNumberPersistentLocalIds.ToList();
            HouseNumber = houseNumber;
            Provenance = provenance;
        }
    }
}
