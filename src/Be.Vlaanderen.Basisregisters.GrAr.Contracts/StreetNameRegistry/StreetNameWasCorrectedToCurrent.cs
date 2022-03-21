namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasCorrectedToCurrent : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasCorrectedToCurrent(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
