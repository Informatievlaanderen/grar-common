namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasRetired : IQueueMessage
    {
        public string StreetNameId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRetired(string streetNameId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            Provenance = provenance;
        }
    }
}
