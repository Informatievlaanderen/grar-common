namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNamePrimaryLanguageWasCorrected : IQueueMessage
    {
        public string StreetNameId { get; }

        public string PrimaryLanguage { get; }

        public Provenance Provenance { get; }

        public StreetNamePrimaryLanguageWasCorrected(string streetNameId,
            string primaryLanguage,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            PrimaryLanguage = primaryLanguage;
            Provenance = provenance;    
        }
    }
}
