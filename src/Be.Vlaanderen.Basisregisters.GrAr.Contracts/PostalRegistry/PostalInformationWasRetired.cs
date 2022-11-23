namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class PostalInformationWasRetired : IQueueMessage
    {
        public string PostalCode { get; }
        public Provenance Provenance { get; }

        public PostalInformationWasRetired(
            string postalCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            Provenance = provenance;
        }
    }
}
