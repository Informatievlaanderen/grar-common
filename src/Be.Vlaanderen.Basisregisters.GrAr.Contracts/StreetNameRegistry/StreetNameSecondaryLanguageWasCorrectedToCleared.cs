namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameSecondaryLanguageWasCorrectedToCleared : IMessage
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
