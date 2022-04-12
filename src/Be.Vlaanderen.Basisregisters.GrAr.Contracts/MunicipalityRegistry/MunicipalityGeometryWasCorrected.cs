namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityGeometryWasCorrected : IMessage
    {
        public string MunicipalityId { get; }
        
        public string ExtendedWkbGeometry { get; }
        
        public Provenance Provenance { get; }

        public MunicipalityGeometryWasCorrected(
            string municipalityId,
            string extendedWkbGeometry,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            ExtendedWkbGeometry = extendedWkbGeometry;
            Provenance = provenance;
        }
    }
}
