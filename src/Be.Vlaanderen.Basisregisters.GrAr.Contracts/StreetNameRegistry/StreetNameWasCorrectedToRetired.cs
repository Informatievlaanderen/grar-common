namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasCorrectedToRetired : IMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasCorrectedToRetired(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
