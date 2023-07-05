namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    /// <summary>
    /// De geometrie.
    /// </summary>
    public class GmlJsonMultiSurface : GmlJsonGeometry
    {
        public GmlJsonMultiSurface(string gml): base("MultiSurface", gml)
        {
        }
    }
}
