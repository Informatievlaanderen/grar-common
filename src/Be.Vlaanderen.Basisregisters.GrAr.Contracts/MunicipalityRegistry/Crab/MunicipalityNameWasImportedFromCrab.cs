namespace MunicipalityRegistry.Municipality.Events
{
    public class MunicipalityNameWasImportedFromCrab
    {
        public int CrabMunicipalityId { get; }
        
        public int CrabMunicipalityNameId { get; }
        
        public string MunicipalityNameName { get; }
        
        public string? MunicipalityNameLanguage { get; }
        
        public string? BeginDateTime { get; }
        
        public string? EndDateTime { get; }
        
        public string Timestamp { get; }
        
        public string Operator { get; }
        
        public string? Modification { get; }
        
        public string? Organisation { get; }

        public MunicipalityNameWasImportedFromCrab(int crabMunicipalityId,
            int crabMunicipalityNameId,
            string municipalityNameName,
            string? municipalityNameLanguage,
            string? beginDateTime,
            string? endDateTime,
            string timestamp,
            string @operator,
            string? modification,
            string? organisation)
        {
            CrabMunicipalityId = crabMunicipalityId;
            CrabMunicipalityNameId = crabMunicipalityNameId;
            MunicipalityNameName = municipalityNameName;
            MunicipalityNameLanguage = municipalityNameLanguage;
            BeginDateTime = beginDateTime;
            EndDateTime = endDateTime;
            Timestamp = timestamp;
            Operator = @operator;
            Modification = modification;
            Organisation = organisation;
        }
    }
}
