namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;

    public class StreetNameHomonymAdditionsWereCorrected : IQueueMessage
    {
        public string MunicipalityId { get; }
        public int PersistentLocalId { get; }
        public IDictionary<string, string> HomonymAdditions { get; }
        public Provenance Provenance { get; }

        public StreetNameHomonymAdditionsWereCorrected(
            string municipalityId,
            int persistentLocalId,
            IDictionary<string, string> homonymAdditions,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            HomonymAdditions = homonymAdditions;
            Provenance = provenance;
        }
    }
}
