namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.MunicipalityRegistry
{
    using Common;

    public class MunicipalityWasRetired : IQueueMessage
    {
        public string MunicipalityId { get; }

        public string RetirementDate { get; }

        public Provenance Provenance { get; }

        public MunicipalityWasRetired(
            string municipalityId,
            string retirementDate,
            Provenance provenance)
        {
            MunicipalityId = municipalityId;
            RetirementDate = retirementDate;
            Provenance = provenance;
        }
    }
}
