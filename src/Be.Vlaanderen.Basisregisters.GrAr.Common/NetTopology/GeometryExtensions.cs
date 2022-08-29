namespace Be.Vlaanderen.Basisregisters.GrAr.Common.NetTopology;

using NetTopologySuite.Geometries;

public static class GeometryExtensions
{
    public static Geometry CentroidWithinArea(this Geometry geometry)
    {
        var centroid = geometry.Centroid;

        return centroid.Within(geometry) ? centroid : geometry.InteriorPoint;
    }
}
