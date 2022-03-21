namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasCorrectedToProposed : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasCorrectedToProposed(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
