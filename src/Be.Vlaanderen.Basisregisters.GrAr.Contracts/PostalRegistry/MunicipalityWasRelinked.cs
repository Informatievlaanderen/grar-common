namespace Be.Vlaanderen.Basisregisters.GrAr.Contracts.PostalRegistry
{
    using Common;

    public class MunicipalityWasRelinked : IQueueMessage
    {
        public string PostalCode { get; }
        public string NewNisCode { get; }

        public string PreviousNisCode { get; }
        public Provenance Provenance { get; }

        public MunicipalityWasRelinked(
            string postalCode,
            string newNisCode,
            string previousNisCode,
            Provenance provenance)
        {
            PostalCode = postalCode;
            NewNisCode = newNisCode;
            PreviousNisCode = previousNisCode;
            Provenance = provenance;
        }
    }
}
