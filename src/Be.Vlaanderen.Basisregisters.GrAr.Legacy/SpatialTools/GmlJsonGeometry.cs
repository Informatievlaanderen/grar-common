namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Newtonsoft.Json;

    /// <summary>
    /// De geometrie.
    /// </summary>
    public class GmlJsonGeometry
    {
        /// <summary>
        /// Het geometrietype.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Type { get; set; }

        /// <summary>
        /// GML3: geometrie object serialisatie.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Gml { get; set; }

        public GmlJsonGeometry(string type, string gml)
        {
            Type = type;
            Gml = gml;
        }
    }
}
