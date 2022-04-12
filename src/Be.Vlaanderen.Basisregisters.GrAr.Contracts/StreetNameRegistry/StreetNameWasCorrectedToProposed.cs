namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasCorrectedToProposed : IMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasCorrectedToProposed(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
