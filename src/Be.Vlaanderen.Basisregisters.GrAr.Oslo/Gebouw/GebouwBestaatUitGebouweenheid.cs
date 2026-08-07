namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouw
{
    using Gebouweenheid;
    using Newtonsoft.Json;

    /// <summary>
    /// De gebouweenheid van het gebouw.
    /// </summary>
    public class GebouwBestaatUitGebouweenheid
    {
        /// <summary>
        /// Het linked-data type van de gebouweenheid.
        /// </summary>
        [JsonProperty("@type", Order = 0, Required = Required.DisallowNull)]
        public string Type => "Gebouweenheid";

        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde gebouweenheid (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Order = 1, Required = Required.DisallowNull)]
        public string Id { get; set; }

        /// <summary>
        /// De status van de gebouweenheid.
        /// </summary>
        [JsonProperty("status", Order = 2, Required = Required.DisallowNull)]
        public GebouweenheidStatus Status { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gebouweenheid weergeeft.
        /// </summary>
        [JsonProperty("detail", Order = 3, Required = Required.DisallowNull)]
        public string Detail { get; set; }

        public GebouwBestaatUitGebouweenheid(
            string id,
            GebouweenheidStatus status,
            string detail)
        {
            Id = id;
            Status = status;
            Detail = detail;
        }

        public GebouwBestaatUitGebouweenheid(
            string id,
            GebouweenheidStatusValue status,
            string detail)
        {
            Id = id;
            Status = new GebouweenheidStatus(status);
            Detail = detail;
        }
    }
}
