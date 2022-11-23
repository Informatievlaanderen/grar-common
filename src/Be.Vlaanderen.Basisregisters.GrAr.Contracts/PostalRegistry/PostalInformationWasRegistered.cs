namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class PostalInformationWasRegistered : IQueueMessage
    {
        public string PostalCode { get; }
        public Provenance Provenance { get; }

        public PostalInformationWasRegistered(
            string postalCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
