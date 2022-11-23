namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class PostalInformationWasRealized : IQueueMessage
    {
        public string PostalCode { get; }
        public Provenance Provenance { get; }

        public PostalInformationWasRealized(
            string postalCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
