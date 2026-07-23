namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Straatnaam
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>De status van de straatnaam.</summary>
    public enum StraatnaamStatusValue
    {
        /// <summary>
        /// Een straatnaam die voorgesteld is.
        /// </summary>
        Voorgesteld = 1,

        /// <summary>
        /// Een straatnaam in gebruik.
        /// </summary>
        InGebruik = 2,

        /// <summary>
        /// Een straatnaam die niet langer in gebruik is.
        /// </summary>
        Gehistoreerd = 3,

        /// <summary>
        /// Een straatnaam die afgekeurd is.
        /// </summary>
        Afgekeurd = 4
    }

    /// <summary>
    /// De status van de straatnaam.
    /// </summary>
    public class StraatnaamStatus
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
        public string Type => "skos:Concept";

        /// <summary>
        /// De beschrijving van de status.
        /// </summary>
        [JsonProperty("skos:prefLabel", Required = Required.DisallowNull, Order = 3)]
        public StraatnaamStatusValue Label { get; set; }

        public StraatnaamStatus(StraatnaamStatusValue straatnaamStatus)
        {
            Label = straatnaamStatus;
            Id = OsloNamespaces.StraatNaamStatus.ToPuri(NamingStrategy.GetPropertyName(straatnaamStatus.ToString(), false));
        }
    }
}
