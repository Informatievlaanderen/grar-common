namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameNameWasCorrected : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public string Name { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCorrected(Guid streetNameId,
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
