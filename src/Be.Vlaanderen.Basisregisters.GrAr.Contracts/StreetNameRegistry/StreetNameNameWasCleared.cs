namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameNameWasCleared : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCleared(Guid streetNameId,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Language = language;
            Provenance = provenance;
        }
    }
}
