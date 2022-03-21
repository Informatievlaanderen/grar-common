namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasNamed : IQueueMessage
    {
        public string StreetNameId { get; }

        public string Name { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameWasNamed(string streetNameId,
            string name,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Name = name;
            Language = language;
            Provenance = provenance;
        }
    }
}
