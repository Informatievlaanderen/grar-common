namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityGeometryWasCleared : IMessage
    {
        public string MunicipalityId { get; }
        
        public Provenance Provenance { get; }

        public MunicipalityGeometryWasCleared(
            string municipalityId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Provenance = provenance;
        }
    }
}
