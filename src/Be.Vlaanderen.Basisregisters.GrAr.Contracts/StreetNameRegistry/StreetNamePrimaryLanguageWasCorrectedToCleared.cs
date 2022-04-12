namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;
    public class StreetNamePrimaryLanguageWasCorrectedToCleared : IMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNamePrimaryLanguageWasCorrectedToCleared(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;    
        }
    }
}
