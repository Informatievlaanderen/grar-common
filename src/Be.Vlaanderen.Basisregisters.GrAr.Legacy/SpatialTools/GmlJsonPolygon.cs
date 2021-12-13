namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// Een geometrie polygon met GML3.
    /// </summary>
    public class GmlJsonPolygon : GmlJsonGeometry
    {
        public GmlJsonPolygon(string gml): base("Polygon", gml)
        {
        }
    }
}
