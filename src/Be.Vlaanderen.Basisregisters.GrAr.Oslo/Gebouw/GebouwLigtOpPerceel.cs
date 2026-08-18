namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouw
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Het perceel waar het gebouw op ligt.
    /// </summary>
    public class GebouwLigtOpPerceel
    {
        /// <summary>
        /// Het linked-data type van het perceel.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type => "Perceel";

        /// <summary>
        /// De unieke en persistente identificator van het gekoppelde perceel (volgt de Vlaamse URI-standaard).
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// De URL die de details van de meest recente versie van het gekoppelde perceel weergeeft.
        /// </summary>
        [JsonProperty("detail", Required = Required.DisallowNull, Order = 2)]
        public Uri Detail { get; set; }

        public GebouwLigtOpPerceel(
            string id,
            Uri detail)
        {
            Id = id;
            Detail = detail;
        }
    }
}
