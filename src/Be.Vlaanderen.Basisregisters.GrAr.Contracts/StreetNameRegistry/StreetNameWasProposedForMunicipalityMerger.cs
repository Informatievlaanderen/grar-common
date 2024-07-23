namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameWasProposedForMunicipalityMerger : IQueueMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public string DesiredStatus { get; }

        public IDictionary<string, string> StreetNameNames { get; }

        public IDictionary<string, string> HomonymAdditions { get; }

        public int PersistentLocalId { get; }

        public List<int> MergedStreetNamePersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public StreetNameWasProposedForMunicipalityMerger(
            string municipalityId,
            string nisCode,
            string desiredStatus,
            IDictionary<string, string> streetNameNames,
            IDictionary<string, string> homonymAdditions,
            int persistentLocalId,
            List<int> mergedStreetNamePersistentLocalIds,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            DesiredStatus = desiredStatus;
            StreetNameNames = streetNameNames;
            HomonymAdditions = homonymAdditions;
            PersistentLocalId = persistentLocalId;
            MergedStreetNamePersistentLocalIds = mergedStreetNamePersistentLocalIds;
            Provenance = provenance;
        }
    }
}
