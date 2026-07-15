namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gemeente
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>De status van de gemeente.</summary>
    public enum GemeenteStatus
    {
        /// <summary>
        /// Een gemeente in gebruik.
        /// </summary>
        InGebruik = 1,

        /// <summary>
        /// Een gemeente die niet langer in gebruik is.
        /// </summary>
        Gehistoreerd = 2,

        /// <summary>
        /// Een gemeente die voorgesteld is.
        /// </summary>
        Voorgesteld = 3
    }

    /// <summary>De status van de gemeente.</summary>
    public class Status
    {
        private static readonly CamelCaseNamingStrategy NamingStrategy = new();

        /// <summary>
        /// Identificatie van de status.
        /// </summary>
        [JsonProperty("@id", Required = Required.DisallowNull, Order = 1)]
        public required string Id { get; set; }

        /// <summary>
        /// Linked data type van het object.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 2)]
        public string Type => "skos:Concept";

        /// <summary>
        /// De beschrijving van de status.
        /// </summary>
        [JsonProperty("skos:prefLabel", Required = Required.DisallowNull, Order = 3)]
        public required GemeenteStatus Label { get; set; }

        public Status(GemeenteStatus gemeenteStatus)
        {
            Label = gemeenteStatus;
            Id = OsloNamespaces.GemeenteStatus.ToPuri(NamingStrategy.GetPropertyName(gemeenteStatus.ToString(), false));
        }
    }
}
