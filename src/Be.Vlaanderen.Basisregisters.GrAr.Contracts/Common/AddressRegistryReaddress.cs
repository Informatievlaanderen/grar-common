namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.Common
{
    public sealed class AddressRegistryReaddress
    {
        public int SourceAddressPersistentLocalId { get; }

        public int DestinationAddressPersistentLocalId { get; }

        public AddressRegistryReaddress(
            int sourceAddressPersistentLocalId,
            int destinationAddressPersistentLocalId)
        {
            SourceAddressPersistentLocalId = sourceAddressPersistentLocalId;
            DestinationAddressPersistentLocalId = destinationAddressPersistentLocalId;
        }
    }
}
