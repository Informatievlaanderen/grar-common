namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Perceel
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// De status van het perceel.
    /// </summary>
    public enum PerceelStatusValue
    {
        /// <summary>
        /// Het perceel is gerealiseerd.
        /// </summary>
        Gerealiseerd = 1,

        /// <summary>
        /// Het perceel is gehistoreerd.
        /// </summary>
        Gehistoreerd = 2,
    }

    /// <summary>De status van het perceel.</summary>
    public class PerceelStatus
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
        public PerceelStatusValue Label { get; set; }

        public PerceelStatus(PerceelStatusValue perceelStatus)
        {
            Label = perceelStatus;
            Id = OsloNamespaces.PerceelStatus.ToPuri(NamingStrategy.GetPropertyName(perceelStatus.ToString(), false));
        }
    }
}
