namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using Newtonsoft.Json;

    /// <summary>
    /// De geografische naam.
    /// </summary>
    public class GeografischeNaam
    {
        /// <summary>
        /// De spelling van de geografische naam in de gespecifieerde taal.
        /// </summary>
        [JsonProperty("@value", Required = Required.DisallowNull, Order = 1)]
        public string Spelling { get; set; }

        /// <summary>
        /// De taal van de geografische naam.
        /// </summary>
        [JsonProperty("@language", Required = Required.DisallowNull, Order = 2)]
        public Taal Taal { get; set; }

        public GeografischeNaam(string spelling, Taal taalCode)
        {
            Spelling = spelling;
            Taal = taalCode;
        }
    }
}
