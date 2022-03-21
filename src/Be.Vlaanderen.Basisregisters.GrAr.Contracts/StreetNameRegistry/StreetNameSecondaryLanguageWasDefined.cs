namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameSecondaryLanguageWasDefined : IQueueMessage
    {
        public string StreetNameId { get; }

        public string SecondaryLanguage { get; }

        public Provenance Provenance { get; }

        public StreetNameSecondaryLanguageWasDefined(string streetNameId,
            string secondaryLanguage,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            SecondaryLanguage = secondaryLanguage;
            Provenance = provenance;
        }
    }
}
