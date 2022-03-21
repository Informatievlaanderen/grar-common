namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameSecondaryLanguageWasCorrected : IQueueMessage
    {
        public string StreetNameId { get; }

        public string SecondaryLanguage { get; }

        public Provenance Provenance { get; }

        public StreetNameSecondaryLanguageWasCorrected(string streetNameId,
            string secondaryLanguage,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            SecondaryLanguage = secondaryLanguage;
            Provenance = provenance;    
        }
    }
}
