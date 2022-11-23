namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class PostalInformationPostalNameWasRemoved : IQueueMessage
    {
        public string PostalCode { get; }
        public string Language { get; }
        public string Name { get; }
        public Provenance Provenance { get; }

        public PostalInformationPostalNameWasRemoved(
            string postalCode,
            string language,
            string name,
            Provenance provenance)
        {
            PostalCode = postalCode;
            Language = language;
            Name = name;
            Provenance = provenance;
        }
    }
}
