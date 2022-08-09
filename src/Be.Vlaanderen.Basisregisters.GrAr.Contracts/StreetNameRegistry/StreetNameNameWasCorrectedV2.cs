namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameNameWasCorrectedV2 : IQueueMessage
    {
        public string MunicipalityId { get; }

        public IDictionary<string, string> StreetNameNames { get; }

        public int PersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameNameWasCorrectedV2(string municipalityId,
            IDictionary<string, string> streetNameNames,
            int persistentLocalId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            StreetNameNames = streetNameNames;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;    
        }
    }
}
