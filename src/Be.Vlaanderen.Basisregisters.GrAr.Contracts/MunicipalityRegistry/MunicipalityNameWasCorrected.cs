namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityNameWasCorrected
    {
        public string MunicipalityId { get; }

        public string Name { get; }

        public string Language { get; }

        public Provenance Provenance { get; }

        public MunicipalityNameWasCorrected(
            string municipalityId,
            string name,
            string language,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Name = name;
            Language = language;
            Provenance = provenance;
        }
    }
}
