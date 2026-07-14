namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Adres
{
    using System.Collections.Generic;
    using Gemeente;
    using Newtonsoft.Json;

    /// <summary>
    /// De gemeente die deel uitmaakt van het adres.
    /// </summary>
    public class AdresHeeftGemeentenaam
    {
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Gemeentenaam";

        /// <summary>
        /// De gemeentenamen van de gemeente.
        /// </summary>
        [JsonProperty("gemeentenaam", Required = Required.DisallowNull, Order = 1)]
        public Gemeentenaam Gemeentenaam { get; set; }

        /// <summary>
        /// De gemeentenaam afgeleid van de gemeente.
        /// </summary>
        [JsonProperty("isAfgeleidVan", Required = Required.DisallowNull, Order = 2)]
        public AdresHeeftGemeentenaamAfgeleidVan IsAfgeleidVan { get; set; }

        public AdresHeeftGemeentenaam(string id, string detail, List<GeografischeNaam> geografischeNamen)
        {
            Gemeentenaam = new Gemeentenaam
            {
                Gemeentenamen = geografischeNamen
            };
            IsAfgeleidVan = new AdresHeeftGemeentenaamAfgeleidVan
            {
                Id = id,
                Detail = detail
            };
        }
    }

    /// <summary>
    /// De gemeentenaam afgeleid van de gemeente.
    /// </summary>
    public class AdresHeeftGemeentenaamAfgeleidVan
    {
        /// <summary>
        /// De unieke en persistente identificator van de gekoppelde gemeente (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van de gekoppelde gemeente weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public string Detail { get; set; }
    }
}
