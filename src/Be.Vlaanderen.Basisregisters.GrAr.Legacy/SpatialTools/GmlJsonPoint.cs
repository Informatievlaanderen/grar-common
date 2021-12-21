namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// De geometrie.
    /// </summary>
    public class GmlJsonPoint : GmlJsonGeometry
    {
        public GmlJsonPoint(string gml): base("Point", gml)
        {
        }
    }
}
