namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameSecondaryLanguageWasCorrected : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public string SecondaryLanguage { get; }

        public Provenance Provenance { get; }

        public StreetNameSecondaryLanguageWasCorrected(Guid streetNameId,
            string secondaryLanguage,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            SecondaryLanguage = secondaryLanguage;
            Provenance = provenance;    
        }
    }
}
