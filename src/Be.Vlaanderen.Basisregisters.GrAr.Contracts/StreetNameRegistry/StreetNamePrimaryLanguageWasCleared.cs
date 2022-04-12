namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNamePrimaryLanguageWasCleared : IMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNamePrimaryLanguageWasCleared(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
