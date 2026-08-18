namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouweenheid
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Het gebouw waar de gebouweenheid toe behoort.
    /// </summary>
    public class GebouweenheidIsDeelVan
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
        public Uri Detail { get; set; }

        public GebouweenheidIsDeelVan(
            string id,
            Uri detail)
        {
            Id = id;
            Detail = detail;
        }
    }
}
