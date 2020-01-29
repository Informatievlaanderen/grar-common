namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using Newtonsoft.Json;

    /// <summary>
    /// Een GeoJSON punt.
    /// </summary>
    public class GeoJSONPoint
    {
        [JsonConverter(typeof(PointCoordinatesConverter))]
        public double[] Coordinates { get; set; }
        public string Type { get; set; }

        public GeoJSONPoint()
        {
            Type = "Point";
        }
    }
}
