namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using Newtonsoft.Json;

    /// <summary>
    /// Het totaal aantal.
    /// </summary>
    public class TotaalAantalResponse
    {
        /// <summary>
        /// Het aantal.
        /// </summary>
        [JsonProperty("aantal", Required = Required.DisallowNull, Order = 1)]
        public int Aantal { get; set; }
    }
}
