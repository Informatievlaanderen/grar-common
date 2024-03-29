namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityBecameCurrent : IQueueMessage
    {
        public string MunicipalityId { get; }

        public Provenance Provenance { get; }

        public MunicipalityBecameCurrent(
            string municipalityId,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            Provenance = provenance;
        }
    }
}
