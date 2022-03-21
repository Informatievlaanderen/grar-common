namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasRegistered : IQueueMessage
    {
        public Guid StreetNameId { get; }

        public Guid MunicipalityId { get; }

        public string NisCode { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRegistered(Guid streetNameId,
            Guid municipalityId,
            string nisCode,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            Provenance = provenance;
        }
    }
}
