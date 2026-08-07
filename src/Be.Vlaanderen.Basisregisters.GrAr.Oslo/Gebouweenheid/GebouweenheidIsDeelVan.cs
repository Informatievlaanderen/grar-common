namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouweenheid
{
    using Newtonsoft.Json;

    /// <summary>
    /// De postinfo die deel uitmaakt van het adres.
    /// </summary>
    public class GebouweenheidDeelVan
    {
        /// <summary>
        /// Het linked-data type van het gebouw.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Gebouw";

        /// <summary>
        /// De unieke en persistente identificator van het gekoppelde gebouw (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde gebouw weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public string Detail { get; set; }

        public GebouweenheidDeelVan(string id,
            string detail)
        {
            Id = id;
            Detail = detail;
        }
    }
}
