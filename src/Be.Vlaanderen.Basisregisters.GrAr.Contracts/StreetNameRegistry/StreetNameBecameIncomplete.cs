namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameBecameIncomplete : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameBecameIncomplete(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
