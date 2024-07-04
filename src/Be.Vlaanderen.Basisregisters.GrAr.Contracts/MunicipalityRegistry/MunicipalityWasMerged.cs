namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using System.Collections.Generic;
    using Common;

    public sealed class MunicipalityWasMerged : IQueueMessage
    {
        public string MunicipalityId { get; }

        public string NisCode { get; }

        public IEnumerable<string> MunicipalityIdsToMergeWith { get; }

        public IEnumerable<string> NisCodesToMergeWith { get; }

        public string NewMunicipalityId { get; }

        public string NewNisCode { get; }

        public Provenance Provenance { get; private set; }

        public MunicipalityWasMerged(
            string municipalityId,
            string nisCode,
            IEnumerable<string> municipalityIdsToMergeWith,
            IEnumerable<string> nisCodesToMergeWith,
            string newMunicipalityId,
            string newNisCode,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            MunicipalityIdsToMergeWith = municipalityIdsToMergeWith;
            NisCodesToMergeWith = nisCodesToMergeWith;
            NewMunicipalityId = newMunicipalityId;
            NewNisCode = newNisCode;
            Provenance = provenance;
        }
    }
}
