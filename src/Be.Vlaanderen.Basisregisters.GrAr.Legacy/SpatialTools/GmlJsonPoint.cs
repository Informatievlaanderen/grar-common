namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// Een geometrie punt met GML3.
    /// </summary>
    public class GmlJsonPoint : GmlJsonGeometry
    {
        public GmlJsonPoint(string gml): base("Point", gml)
        {
        }
    }
}
