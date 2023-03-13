namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameNamesWereChanged : IQueueMessage
    {
        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }

        public IDictionary<string, string> StreetNameNames { get; }


        public Provenance Provenance { get; }

        public StreetNameNamesWereChanged(string municipalityId,
            int persistentLocalId,
            IDictionary<string, string> streetNameNames,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            StreetNameNames = streetNameNames;
            Provenance = provenance;    
        }
    }
}
