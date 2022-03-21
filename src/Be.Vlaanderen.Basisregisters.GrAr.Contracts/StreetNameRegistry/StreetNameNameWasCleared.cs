namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameNameWasCleared : IQueueMessage
    {
        public string StreetNameId { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCleared(string streetNameId,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Language = language;
            Provenance = provenance;
        }
    }
}
