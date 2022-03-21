namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNamePrimaryLanguageWasDefined : IQueueMessage
    {
        public string StreetNameId { get; }

        public string PrimaryLanguage { get; }

        public Provenance Provenance { get; }

        public StreetNamePrimaryLanguageWasDefined(string streetNameId,
            string primaryLanguage,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            PrimaryLanguage = primaryLanguage;
            Provenance = provenance;
        }
    }
}
