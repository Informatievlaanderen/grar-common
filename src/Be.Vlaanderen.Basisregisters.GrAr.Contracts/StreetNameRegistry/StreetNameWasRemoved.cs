namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasRemoved : IMessage
    {
        public string StreetNameId { get; }
        public Provenance Provenance { get; }

        public StreetNameWasRemoved(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;    
        }
    }
}
