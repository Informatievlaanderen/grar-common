namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// Een GeoJSON polygoon.
    /// </summary>
    public class GeoJSONPolygon
    {
        /// <summary>
        /// Coördinaten volgens Lambert-72 (EPSG:31370).
        /// </summary>
        public double[][][] Coordinates { get; set; }

        /// <summary>
        /// GeoJSON-geometrietype.
        /// </summary>
        public string Type { get; set; }

        public GeoJSONPolygon()
        {
            Type = "Polygon";
        }
    }
}
