namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Adres
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// De straatnaam die deel uitmaakt van het adres.
    /// </summary>
    public class AdresHeeftStraatnaam
    {
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Straatnaam";

        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde straatnaam (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde straatnaam weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public string Detail { get; set; }

        /// <summary>
        /// De straatnamen in de officiële talen van de gemeente.
        /// </summary>
        [JsonProperty("straatnaam", Required = Required.DisallowNull, Order = 3)]
        public List<GeografischeNaam> Straatnaam { get; set; }

        /// <summary>
        /// De homonieme toevoegingen van de straatnaam.
        /// </summary>
        [JsonProperty("homoniemToevoeging", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore, Order = 4)]
        public List<GeografischeNaam>? HomoniemToevoeging { get; set; }

        public AdresHeeftStraatnaam(string id, string detail, List<GeografischeNaam> straatnamen, List<GeografischeNaam>? homoniemToevoeging = null)
        {
            Id = id;
            Detail = detail;
            Straatnaam = straatnamen;
            HomoniemToevoeging = homoniemToevoeging;
        }
    }
}
