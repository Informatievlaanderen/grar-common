namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Perceel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Het toegekende adres aan het perceel.
    /// </summary>
    public class PerceelToegekendAdres
    {
        /// <summary>
        /// Het linked-data type van het adres.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Adres";

        /// <summary>
        /// De unieke en persistente identificator van het gekoppelde adres (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde adres weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public string Detail { get; set; }
    }
}
