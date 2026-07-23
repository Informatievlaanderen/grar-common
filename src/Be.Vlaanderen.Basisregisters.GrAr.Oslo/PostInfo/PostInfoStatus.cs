namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.PostInfo
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// De status van de postinfo.
    /// </summary>
    public enum PostInfoStatusValue
    {
        /// <summary>
        /// Een gerealiseerd object.
        /// </summary>
        Gerealiseerd = 1,

        /// <summary>
        /// Een gehistoreerd object.
        /// </summary>
        Gehistoreerd = 2
    }

    /// <summary>
    /// De status van de postinfo.
    /// </summary>
    public class PostInfoStatus
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
        public PostInfoStatusValue Label { get; set; }

        public PostInfoStatus(PostInfoStatusValue postInfoStatus)
        {
            Label = postInfoStatus;
            Id = OsloNamespaces.PostinfoStatus.ToPuri(NamingStrategy.GetPropertyName(postInfoStatus.ToString(), false));
        }
    }
}
