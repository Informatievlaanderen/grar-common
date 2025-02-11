namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using Common;

    public class AddressBoxNumbersWereCorrected : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public IDictionary<int, string> AddressBoxNumbers { get; }

        public Provenance Provenance { get; }

        public AddressBoxNumbersWereCorrected(
            int streetNamePersistentLocalId,
            IDictionary<int, string> addressBoxNumbers,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            AddressBoxNumbers = addressBoxNumbers;
            Provenance = provenance;
        }
    }
}
