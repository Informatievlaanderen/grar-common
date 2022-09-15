namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public class AddressPostalCodeWasCorrectedV2 : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public int AddressPersistentLocalId { get; }

        public List<int> BoxNumberPersistentLocalIds { get; }

        public string PostalCode { get; }

        public Provenance Provenance { get; }

        public AddressPostalCodeWasCorrectedV2(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            IEnumerable<int> boxNumberPersistentLocalIds,
            string postalCode,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressPersistentLocalId = addressPersistentLocalId;
            BoxNumberPersistentLocalIds = boxNumberPersistentLocalIds.ToList();
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
