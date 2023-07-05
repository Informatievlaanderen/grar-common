namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Common.SpatialTools.GeometryCoordinates;
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

        public GeoJSONPoint()
        {
            Type = "Point";
        }
    }
}
