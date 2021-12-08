namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Newtonsoft.Json;

    /// <summary>
    /// Een GeoJSON punt.
    /// </summary>
    public class GeoJSONPoint
    {
        /// <summary>
        /// Coördinaten volgens Lambert-72 (EPSG:31370).
        /// </summary>
        [JsonConverter(typeof(PointCoordinatesConverter))]
        [JsonProperty(Required = Required.DisallowNull)]
        public double[] Coordinates { get; set; }


        /// <summary>
        /// GeoJSON-geometrietype.
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public string Type { get; set; }

        /// <summary>
        /// Coördinaten volgens Lambert-72 (EPSG:31370).
        /// </summary>
        [JsonConverter(typeof(GmlPointConverter))]
        [JsonProperty(Required = Required.DisallowNull)]
        public double[] Gml => Coordinates;

        public GeoJSONPoint()
        {
            Type = "Point";
        }
    }
}
