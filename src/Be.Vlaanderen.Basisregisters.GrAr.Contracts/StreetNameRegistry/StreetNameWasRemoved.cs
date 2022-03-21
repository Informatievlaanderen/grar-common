namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasRemoved : IQueueMessage
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
