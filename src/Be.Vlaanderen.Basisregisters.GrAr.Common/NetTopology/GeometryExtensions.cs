namespace Be.Vlaanderen.Basisregisters.GrAr.Common.NetTopology;

using System;
using System.Linq;
using System.Text;
using System.Xml;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using SpatialTools.GeometryCoordinates;

public static class GeometryExtensions
{
    public static Geometry CentroidWithinArea(this Geometry geometry)
    {
        var centroid = geometry.Centroid;

        return centroid.Within(geometry) ? centroid : geometry.InteriorPoint;
    }

    private const string GmlNamespace = "http://www.opengis.net/gml/3.2";
    private const string SrsName = "https://www.opengis.net/def/crs/EPSG/0/31370";

    public static string ConvertToGml(this Geometry geometry)
    {
        if (geometry is not Polygon && geometry is not MultiPolygon && geometry is not Point)
            throw new InvalidOperationException();

        var builder = new StringBuilder();
        var settings = new XmlWriterSettings {Indent = false, OmitXmlDeclaration = true};

        if (geometry is Polygon polygon)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "Polygon", GmlNamespace);
                xmlwriter.WriteAttributeString("srsName", SrsName);
                WriteRing((polygon.ExteriorRing as LinearRing)!, xmlwriter);
                WriteInteriorRings(polygon.InteriorRings, polygon.NumInteriorRings, xmlwriter);
                xmlwriter.WriteEndElement();
            }
        }
        else if (geometry is MultiPolygon multiPolygon)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "MultiSurface", GmlNamespace);
                xmlwriter.WriteAttributeString("srsName", SrsName);

                foreach (var p in multiPolygon.Geometries.Cast<Polygon>())
                {
                    xmlwriter.WriteStartElement("gml", "surfaceMember", null!);
                    xmlwriter.WriteStartElement("gml", "Polygon", null!);

                    WriteRing((p.ExteriorRing as LinearRing)!, xmlwriter);
                    WriteInteriorRings(p.InteriorRings, p.NumInteriorRings, xmlwriter);

                    xmlwriter.WriteEndElement();
                    xmlwriter.WriteEndElement();
                }

                xmlwriter.WriteEndElement();
            }
        }
        else if (geometry is Point point)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "Point", GmlNamespace);
                xmlwriter.WriteAttributeString("srsName", SrsName);

                xmlwriter.WriteStartElement("gml", "pos", null!);
                xmlwriter.WriteValue(string.Format(Global.GetNfi(), "{0} {1}",
                    point.Coordinate.X.ToPointGeometryCoordinateValueFormat(),
                    point.Coordinate.Y.ToPointGeometryCoordinateValueFormat()));
                xmlwriter.WriteEndElement();

                xmlwriter.WriteEndElement();
            }
        }

        return builder.ToString();
    }

    private static void WriteRing(
        LinearRing ring,
        XmlWriter writer,
        bool isInterior = false)
    {
        writer.WriteStartElement("gml", isInterior ? "interior" : "exterior", GmlNamespace);
        writer.WriteStartElement("gml", "LinearRing", GmlNamespace);
        writer.WriteStartElement("gml", "posList", GmlNamespace);

        var posListBuilder = new StringBuilder();
        foreach (var coordinate in ring.Coordinates)
        {
            posListBuilder.Append(string.Format(
                Global.GetNfi(),
                "{0} {1} ",
                coordinate.X.ToPolygonGeometryCoordinateValueFormat(),
                coordinate.Y.ToPolygonGeometryCoordinateValueFormat()));
        }

        //remove last space
        posListBuilder.Length--;

        writer.WriteValue(posListBuilder.ToString());

        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static void WriteInteriorRings(
        LineString[] rings,
        int numInteriorRings,
        XmlWriter writer)
    {
        if (numInteriorRings < 1)
        {
            return;
        }

        foreach (var ring in rings)
        {
            WriteRing((ring as LinearRing)!, writer, true);
        }
    }
}
