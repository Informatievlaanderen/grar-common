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
        public string Gml => GmlHelper.ToGmlPointString(Coordinates);

        public GmlJSONPoint()
        {
            Type = "Point";
        }

        public static implicit operator GeoJSONPoint(GmlJSONPoint p) => p.ToGeoJSONPoint();

        public static implicit operator GmlJSONPoint(GeoJSONPoint p) => p.ToGmlJSONPoint();
    }
}
