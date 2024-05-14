namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using System.Collections.Generic;
    using Common;

    public class StreetNameWasReaddressed : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }

        public IReadOnlyList<AddressHouseNumberReaddressedData> ReaddressedHouseNumbers { get; }

        public Provenance Provenance { get; }

        public StreetNameWasReaddressed(
            int streetNamePersistentLocalId,
            IReadOnlyList<AddressHouseNumberReaddressedData> readdressedHouseNumbers,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            ReaddressedHouseNumbers = readdressedHouseNumbers;
            Provenance = provenance;
        }
    }

    public class AddressHouseNumberReaddressedData
    {
        public int AddressPersistentLocalId { get; }

        public ReaddressedAddressData ReaddressedHouseNumber { get; }

        public IReadOnlyList<ReaddressedAddressData> ReaddressedBoxNumbers { get; }

        public AddressHouseNumberReaddressedData(
            int addressPersistentLocalId,
            ReaddressedAddressData readdressedHouseNumber,
            IReadOnlyList<ReaddressedAddressData> readdressedBoxNumbers)
        {
            AddressPersistentLocalId = addressPersistentLocalId;
            ReaddressedHouseNumber = readdressedHouseNumber;
            ReaddressedBoxNumbers = readdressedBoxNumbers;
        }
    }
}
