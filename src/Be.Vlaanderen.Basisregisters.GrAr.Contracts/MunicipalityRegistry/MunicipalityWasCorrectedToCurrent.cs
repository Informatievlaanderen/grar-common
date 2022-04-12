namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasCorrectedToCurrent : IMessage
    {
        public string MunicipalityId { get; }

        public Provenance Provenance { get; }

        public MunicipalityWasCorrectedToCurrent(
            string municipalityId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Provenance = provenance;
        }
    }
}
