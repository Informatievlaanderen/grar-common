namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasRetired : IMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRetired(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
