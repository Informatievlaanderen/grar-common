namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameSecondaryLanguageWasCorrectedToCleared : IQueueMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameSecondaryLanguageWasCorrectedToCleared(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
