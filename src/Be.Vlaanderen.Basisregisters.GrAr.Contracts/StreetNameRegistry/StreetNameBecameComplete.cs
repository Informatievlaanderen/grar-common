namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameBecameComplete : IQueueMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameBecameComplete(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
