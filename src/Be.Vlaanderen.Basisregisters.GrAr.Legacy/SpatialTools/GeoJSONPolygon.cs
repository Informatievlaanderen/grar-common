namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    public class GeoJSONPolygon
    {
        public double[][][] Coordinates { get; set; }
        public string Type { get; set; }

        public GeoJSONPolygon()
        {
            Type = "Polygon";
        }
    }
}
