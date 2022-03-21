namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasRemoved : IQueueMessage
    {
        public Guid StreetNameId { get; }
        public Provenance Provenance { get; }

        public StreetNameWasRemoved(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;    
        }
    }
}
