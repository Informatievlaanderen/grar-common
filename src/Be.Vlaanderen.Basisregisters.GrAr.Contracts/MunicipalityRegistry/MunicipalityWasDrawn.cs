namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasDrawn
    {
        public string MunicipalityId { get; }
        
        public string ExtendedWkbGeometry { get; }
        
        public Provenance Provenance { get; }

        public MunicipalityWasDrawn(
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
