namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Een GeoJSON punt met gml.
    /// </summary>
    public class GmlJSONPoint
    {
        /// <summary>
        /// GeoJSON-geometrietype.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Type { get; set; }

        /// <summary>
        /// Coördinaten volgens Lambert-72 (EPSG:31370).
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Gml { get; set; }

        public GmlJSONPoint()
        {
            Type = "Point";
        }

        public GmlJSONPoint(string gml): this()
        {
            Gml = gml;
        }
    }
}
