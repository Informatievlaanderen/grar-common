namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouw
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// De status van het gebouw.
    /// </summary>
    public enum GebouwStatusValue
    {
        /// <summary>
        /// Bouwaanvraag is aangevraagd of toegekend voor het gebouw.
        /// </summary>
        Gepland = 1,

        /// <summary>
        /// Het gebouw wordt gebouwd.
        /// </summary>
        InAanbouw = 2,

        /// <summary>
        /// Het gebouw is gerealiseerd of is observeerbaar.
        /// </summary>
        Gerealiseerd = 3,

        /// <summary>
        /// Het gebouw is geslopen, gesplitst of samengevoegd.
        /// </summary>
        Gehistoreerd = 4,

        /// <summary>
        /// De bouwaanvraag is niet aangevraagd, geannuleerd of verlopen.
        /// </summary>
        NietGerealiseerd = 5
    }

    /// <summary>De status van het gebouw.</summary>
    public class GebouwStatus
    {
        private static readonly CamelCaseNamingStrategy NamingStrategy = new();

        /// <summary>
        /// Identificatie van de status.
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 2)]
        public string Type => "Concept";

        /// <summary>
        /// De code notatie van de status.
        /// </summary>
        [JsonProperty("code", Required = Required.DisallowNull, Order = 3)]
        public GebouwStatusValue Label { get; set; }

        public GebouwStatus(GebouwStatusValue gebouwStatus)
        {
            Label = gebouwStatus;
            Id = OsloNamespaces.GebouwStatus.ToPuri(NamingStrategy.GetPropertyName(gebouwStatus.ToString(), false));
        }
    }
}
