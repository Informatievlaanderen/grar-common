namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gebouweenheid
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// De status van de gebouweenheid.
    /// </summary>
    public enum GebouweenheidStatusValue
    {
        /// <summary>
        /// Een bouwaanvraag is toegekend voor de gebouweenheid.
        /// </summary>
        Gepland = 1,

        /// <summary>
        /// De gebouweenheid is gerealiseerd (werk afgerond) en is observeerbaar.
        /// </summary>
        Gerealiseerd = 2,

        /// <summary>
        /// De gebouweenheid is gesloopt, gesplitst of samengevoegd.
        /// </summary>
        Gehistoreerd = 3,

        /// <summary>
        /// De bouwaanvraag voor de gebouweenheid is niet toegekend, geannuleerd of verlopen.
        /// </summary>
        NietGerealiseerd = 4
    }

    /// <summary>De status van de gebouweenheid.</summary>
    public class GebouweenheidStatus
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
        public GebouweenheidStatusValue Label { get; set; }

        public GebouweenheidStatus(GebouweenheidStatusValue gebouweenheidStatus)
        {
            Label = gebouweenheidStatus;
            Id = OsloNamespaces.GebouweenheidStatus.ToPuri(NamingStrategy.GetPropertyName(gebouweenheidStatus.ToString(), false));
        }
    }
}
