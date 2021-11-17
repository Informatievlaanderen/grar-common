namespace MunicipalityRegistry.Municipality.Events
{
    public class MunicipalityWasImportedFromCrab
    {
        public int CrabMunicipalityId { get; }
        
        public string NisCode { get; }
        
        public string? PrimaryLanguage { get; }
        
        public string? SecondaryLanguage { get; }
        
        public int? NumberOfFlags { get; }
        
        public string? BeginDate { get; }
        
        public string? EndDate { get; }
        
        public string WkbGeometry { get; }
        
        public string Timestamp { get; }
        
        public string Operator { get; }
        
        public string? Modification { get; }
        
        public string? Organisation { get; }

        public MunicipalityWasImportedFromCrab(int crabMunicipalityId,
            string nisCode,
            string? primaryLanguage,
            string? secondaryLanguage,
            int? numberOfFlags,
            string? beginDate,
            string? endDate,
            string wkbGeometry,
            string timestamp,
            string @operator,
            string? modification,
            string? organisation)
        {
            CrabMunicipalityId = crabMunicipalityId;
            NisCode = nisCode;
            PrimaryLanguage = primaryLanguage;
            SecondaryLanguage = secondaryLanguage;
            NumberOfFlags = numberOfFlags;
            BeginDate = beginDate;
            EndDate = endDate;
            WkbGeometry = wkbGeometry;
            Timestamp = timestamp;
            Operator = @operator;
            Modification = modification;
            Organisation = organisation;
        }
    }
}
