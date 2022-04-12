namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameHomonymAdditionWasCleared : IMessage
    {
        public string StreetNameId { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameHomonymAdditionWasCleared(string streetNameId,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Language = language;
            Provenance = provenance;    
        }
    }
}
