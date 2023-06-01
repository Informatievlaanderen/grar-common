namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Newtonsoft.Json;

    /// <summary>
    /// Een GeoJSON linestring.
    /// </summary>
    public class GeoJSONLineString
    {
        /// <summary>
        /// Coördinaten volgens Lambert-72 (EPSG:31370).
        /// </summary>
        [JsonConverter(typeof(LineStringCoordinatesConverter))]
        [JsonProperty(Required = Required.DisallowNull)]
        public double[][] Coordinates { get; set; }
        
        /// <summary>
        /// GeoJSON-geometrietype.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Type { get; }

        public GeoJSONLineString()
        {
            Type = "LineString";
        }
    }
}
