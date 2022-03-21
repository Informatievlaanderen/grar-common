namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameNameWasCorrected : IQueueMessage
    {
        public string StreetNameId { get; }

        public string Name { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCorrected(string streetNameId,
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
