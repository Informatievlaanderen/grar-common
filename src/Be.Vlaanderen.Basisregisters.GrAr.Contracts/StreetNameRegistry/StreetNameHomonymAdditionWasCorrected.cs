namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameHomonymAdditionWasCorrected : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public string HomonymAddition { get; }

        public string? Language { get; }

        public Provenance Provenance { get; }

        public StreetNameHomonymAdditionWasCorrected(Guid streetNameId,
            string homonymAddition,
            string? language,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            HomonymAddition = homonymAddition;
            Language = language;
            Provenance = provenance;    
        }
    }
}
