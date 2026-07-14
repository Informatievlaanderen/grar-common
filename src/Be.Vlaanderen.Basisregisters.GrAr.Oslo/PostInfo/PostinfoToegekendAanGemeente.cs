namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.PostInfo
{
    using Gemeente;
    using Newtonsoft.Json;

    /// <summary>
    /// De gemeente aan dewelke de postinfo is toegewezen.
    /// </summary>
    public class PostinfoToegekendAanGemeente
    {
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Gemeente";

        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde gemeente (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public required string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gemeente weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public required string Detail { get; set; }

        /// <summary>
        /// De gemeentenamen van de gemeente.
        /// </summary>
        [JsonProperty("naam", Required = Required.DisallowNull, Order = 3)]
        public required Gemeentenaam Gemeentenaam { get; set; }
    }
}
