namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.AddressRegistry
{
    using Common;

    public class MigratedStreetNameWasImported : IQueueMessage
    {
        public string StreetNameId { get; }

        public int StreetNamePersistentLocalId { get; }

        public string MunicipalityId { get; }

        public string NisCode { get; }

        public string StreetNameStatus { get; }

        public Provenance Provenance { get; }

        public MigratedStreetNameWasImported(string streetNameId,
            int streetNamePersistentLocalId,
            string municipalityId,
            string nisCode,
            string streetNameStatus,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            StreetNameStatus = streetNameStatus;
            Provenance = provenance;
        }
    }
}
