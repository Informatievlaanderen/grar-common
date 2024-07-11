namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class StreetNameWasRetiredBecauseOfMunicipalityMerger : IQueueMessage
    {
        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }

        public IReadOnlyList<int> NewPersistentLocalIds { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRetiredBecauseOfMunicipalityMerger(
            string municipalityId,
            int persistentLocalId,
            IReadOnlyList<int> newPersistentLocalIds,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            NewPersistentLocalIds = newPersistentLocalIds;
            Provenance = provenance;
        }
    }
}
