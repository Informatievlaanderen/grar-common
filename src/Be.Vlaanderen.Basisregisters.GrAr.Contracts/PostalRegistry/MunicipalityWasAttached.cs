namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class MunicipalityWasAttached : IQueueMessage
    {
        public string PostalCode { get; }
        public string NisCode { get; }
        public Provenance Provenance { get; }

        public MunicipalityWasAttached(
            string postalCode,
            string nisCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            NisCode = nisCode;
            Provenance = provenance;
        }
    }
}
