namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameWasProposedV2 : IMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public IDictionary<string, string> StreetNameNames { get; }

        public int PersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasProposedV2(string municipalityId,
            string nisCode,
            IDictionary<string, string> streetNameNames,
            int persistentLocalId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            StreetNameNames = streetNameNames;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;    
        }
    }
}
