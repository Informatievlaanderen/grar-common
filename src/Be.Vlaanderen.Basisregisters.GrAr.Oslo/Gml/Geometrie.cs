namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.Gml
{
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie.
    /// </summary>
    public class Geometrie
    {
        /// <summary>
        /// Het geometrietype.
        /// </summary>
        [JsonProperty("@type", Required = Required.DisallowNull, Order = 0)]
        public string Type { get; set; }

        /// <summary>
        /// GML3: geometrie object serialisatie.
        /// </summary>
        [JsonProperty("gml", Required = Required.DisallowNull, Order = 1)]
        public string Gml { get; set; }

        public Geometrie(
            string type,
            string gml)
        {
            Type = type;
            Gml = gml;
        }
    }
}
