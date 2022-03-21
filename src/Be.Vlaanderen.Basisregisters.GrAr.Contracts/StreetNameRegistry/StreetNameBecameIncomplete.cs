namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameBecameIncomplete : IQueueMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameBecameIncomplete(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
