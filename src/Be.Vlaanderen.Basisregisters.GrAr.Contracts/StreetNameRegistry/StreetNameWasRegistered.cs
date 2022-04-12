namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.StreetNameRegistry
{
    using Common;

    public class StreetNameWasRegistered : IMessage
    {
        public string StreetNameId { get; }

        public string MunicipalityId { get; }

        public string NisCode { get; }

        public Provenance Provenance { get; }

        public StreetNameWasRegistered(string streetNameId,
            string municipalityId,
            string nisCode,
            Provenance provenance)
        {
            StreetNameId = streetNameId;
            MunicipalityId = municipalityId;
            NisCode = nisCode;
            Provenance = provenance;
        }
    }
}
