namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// De geometrie.
    /// </summary>
    public class GmlJsonPolygon : GmlJsonGeometry
    {
        public GmlJsonPolygon(string gml): base("Polygon", gml)
        {
        }
    }
}
