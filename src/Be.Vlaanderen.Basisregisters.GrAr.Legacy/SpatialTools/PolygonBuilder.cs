﻿namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common.SpatialTools.GeometryCoordinates;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO;

    public static class GmlMultiSurfaceBuilder
    {
        public static GmlMultiSurface? Build(
            byte[] multiSurfaceGmlBytes,
            WKBReader wkbReader)
        {
            var multiPolygon = wkbReader.Read(multiSurfaceGmlBytes) as MultiPolygon;

            if (multiPolygon is null)
                return null;

            var surfaceMembers = new List<GmlSurfaceMember>();
            foreach (var polygon in multiPolygon.Geometries.Cast<NetTopologySuite.Geometries.Polygon>())
            {
                var gmlPolygon = PolygonBuilder.MapGmlPolygon(polygon);
                surfaceMembers.Add(new GmlSurfaceMember {Polygon = gmlPolygon});
            }

            return new GmlMultiSurface(surfaceMembers);
        }
    }

    public static class PolygonBuilder
    {
        public static Polygon? Build(
            byte[] polygon,
            WKBReader wkbReader)
        {
            var geometry = wkbReader.Read(polygon) as NetTopologySuite.Geometries.Polygon;

            if (geometry == null) //some buildings have multi polygons (imported) which are incorrect.
            {
                return null;
            }

            return new Polygon
            {
                XmlPolygon = MapGmlPolygon(geometry),
                JsonPolygon = MapToGeoJsonPolygon(geometry)
            };
        }

        private static GeoJSONPolygon MapToGeoJsonPolygon(NetTopologySuite.Geometries.Polygon polygon)
        {
            var rings = polygon.InteriorRings.ToList();
            rings.Insert(0, polygon.ExteriorRing); //insert exterior ring as first item

            var output = new double[rings.Count][][];
            for (var i = 0; i < rings.Count; i++)
            {
                output[i] = new double[rings[i].Coordinates.Length][];

                for (int j = 0; j < rings[i].Coordinates.Length; j++)
                {
                    output[i][j] = new double[2];
                    output[i][j][0] = rings[i].Coordinates[j].X;
                    output[i][j][1] = rings[i].Coordinates[j].Y;
                }
            }

            return new GeoJSONPolygon {Coordinates = output};
        }

        internal static GmlPolygon MapGmlPolygon(NetTopologySuite.Geometries.Polygon polygon)
        {
            var gmlPolygon = new GmlPolygon
            {
                Exterior = GetGmlRing((polygon.ExteriorRing as NetTopologySuite.Geometries.LinearRing)!)
            };

            if (polygon.NumInteriorRings > 0)
            {
                gmlPolygon.Interior = new List<RingProperty>();
            }

            for (var i = 0; i < polygon.NumInteriorRings; i++)
            {
                gmlPolygon.Interior.Add(GetGmlRing((polygon.InteriorRings[i] as NetTopologySuite.Geometries.LinearRing)!));
            }

            return gmlPolygon;
        }

        private static RingProperty GetGmlRing(NetTopologySuite.Geometries.LinearRing ring)
        {
            var posListBuilder = new StringBuilder();
            foreach (var coordinate in ring.Coordinates)
            {
                posListBuilder.Append($"{coordinate.X.ToPolygonGeometryCoordinateValueFormat()} {coordinate.Y.ToPolygonGeometryCoordinateValueFormat()} ");
            }

            //remove last space
            posListBuilder.Length--;

            var gmlRing = new RingProperty {LinearRing = new LinearRing {PosList = posListBuilder.ToString()}};
            return gmlRing;
        }
    }
}
