namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityGeometryWasCorrectedToCleared : IMessage
    {
        public string MunicipalityId { get; }

        public Provenance Provenance { get; }

        public MunicipalityGeometryWasCorrectedToCleared(
            string municipalityId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Provenance = provenance;
        }
    }
}
