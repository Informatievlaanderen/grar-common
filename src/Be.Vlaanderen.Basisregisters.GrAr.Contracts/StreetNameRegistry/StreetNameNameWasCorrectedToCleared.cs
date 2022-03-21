namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameNameWasCorrectedToCleared : IQueueMessage
    {
        public string StreetNameId { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCorrectedToCleared(string streetNameId,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Language = language;
            Provenance = provenance;
        }
    }
}
