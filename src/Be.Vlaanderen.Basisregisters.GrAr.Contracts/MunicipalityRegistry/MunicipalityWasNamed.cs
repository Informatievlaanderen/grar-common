namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasNamed : IMessage
    {
        public string MunicipalityId { get; }

        public string Name { get; }

        public string Language { get; }

        public Provenance Provenance { get; }

        public MunicipalityWasNamed(
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
