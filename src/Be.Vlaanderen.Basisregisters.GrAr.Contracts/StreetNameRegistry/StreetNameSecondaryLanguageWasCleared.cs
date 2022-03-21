namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameSecondaryLanguageWasCleared : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameSecondaryLanguageWasCleared(Guid streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;    
        }
    }
}
