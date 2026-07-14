namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Adres
{
    using Newtonsoft.Json;

    /// <summary>
    /// De postinfo die deel uitmaakt van het adres.
    /// </summary>
    public class AdresHeeftPostinfo
    {
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "PostInfo";

        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde postinfo (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde postinfo weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public string Detail { get; set; }

        public AdresHeeftPostinfo(string id, string detail)
        {
            Id = id;
            Detail = detail;
        }
    }
}
