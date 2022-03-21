namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasProposed : IQueueMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasProposed(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
