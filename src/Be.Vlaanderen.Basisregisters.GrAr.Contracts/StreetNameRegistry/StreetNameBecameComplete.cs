namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameBecameComplete : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameBecameComplete(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
