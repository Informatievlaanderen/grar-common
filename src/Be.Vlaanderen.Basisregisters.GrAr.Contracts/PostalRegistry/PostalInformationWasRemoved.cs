namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class PostalInformationWasRemoved : IQueueMessage
    {
        public string PostalCode { get; }
        public Provenance Provenance { get; }

        public PostalInformationWasRemoved(
            string postalCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
