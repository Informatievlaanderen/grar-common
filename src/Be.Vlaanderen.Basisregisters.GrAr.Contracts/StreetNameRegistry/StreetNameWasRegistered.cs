namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System;
    using Common;

    public class StreetNameWasRegistered : IQueueMessage
    {
        public string StreetNameId { get; }

        public Guid MunicipalityId { get; }

        public string NisCode { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRegistered(string streetNameId,
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
