namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// Een GeoJSON punt.
    /// </summary>
    public class GeoJSONPoint
    {
        public double[] Coordinates { get; set; }
        public string Type { get; set; }

        public GeoJSONPoint()
        {
            Type = "Point";
        }
    }
}
