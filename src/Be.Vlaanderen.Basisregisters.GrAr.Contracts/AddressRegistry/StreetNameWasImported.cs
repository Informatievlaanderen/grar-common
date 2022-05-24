namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class StreetNameWasImported : IQueueMessage
    {
        public int StreetNamePersistentLocalId { get; }
        public string MunicipalityId { get; }
        public string StreetNameStatus { get; }
        public Provenance Provenance { get; }

        public StreetNameWasImported(int streetNamePersistentLocalId,
            string municipalityId,
            string streetNameStatus,
            Provenance provenance)
        {
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            MunicipalityId = municipalityId;
            StreetNameStatus = streetNameStatus;
            Provenance = provenance;
        }
    }
}

