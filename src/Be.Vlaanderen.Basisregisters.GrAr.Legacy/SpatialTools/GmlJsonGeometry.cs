namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Newtonsoft.Json;

    /// <summary>
    /// Een geometrie met GML3.
    /// </summary>
    public class GmlJsonGeometry
    {
        /// <summary>
        /// Geometrietype.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Type { get; set; }

        /// <summary>
        /// GML3: geometrie object serializatie.
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
