namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;

    public class MunicipalityNisCodeWasChanged : IQueueMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public IReadOnlyList<int> StreetNamePersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public MunicipalityNisCodeWasChanged(
            string municipalityId,
            string nisCode,
            IReadOnlyList<int> streetNamePersistentLocalIds,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            StreetNamePersistentLocalIds = streetNamePersistentLocalIds;
            Provenance = provenance;
        }
    }
}
