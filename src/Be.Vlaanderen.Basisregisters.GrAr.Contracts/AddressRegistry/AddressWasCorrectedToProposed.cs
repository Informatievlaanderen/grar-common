namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class AddressWasCorrectedToProposed : IQueueMessage
    {
        public string AddressId { get; }
        
        public Provenance Provenance { get; }
    }
}
