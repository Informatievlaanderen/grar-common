namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;

    public class StreetNameHomonymAdditionsWereRemoved : IQueueMessage
    {
        public string MunicipalityId { get; }
        public int PersistentLocalId { get; }
        public List<string> Languages { get; }
        public Provenance Provenance { get; }

        public StreetNameHomonymAdditionsWereRemoved(
            string municipalityId,
            int persistentLocalId,
            List<string> languages,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            Languages = languages;
            Provenance = provenance;
        }
    }
}
