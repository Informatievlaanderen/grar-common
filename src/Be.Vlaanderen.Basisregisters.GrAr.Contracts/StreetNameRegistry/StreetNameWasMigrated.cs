namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasMigrated : IQueueMessage
    {
        public string StreetNameId { get; }

        public string MunicipalityId { get; }

        public int PersistentLocalId { get; }

        public Provenance Provenance { get; }

        public StreetNameWasMigrated(string streetNameId,
            string municipalityId,
            int persistentLocalId,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            MunicipalityId = municipalityId;
            PersistentLocalId = persistentLocalId;
            Provenance = provenance;    
        }
    }
}
