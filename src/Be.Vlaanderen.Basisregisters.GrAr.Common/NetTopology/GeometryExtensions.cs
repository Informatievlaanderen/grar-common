namespace Be.Vlaanderen.Basisregisters.GrAr.Common.NetTopology;

using System;
using System.Globalization;
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

    public static T WithSrid<T>(this T geometry, int srid)
        where T : Geometry
    {
        if(srid <= 0)
            throw new ArgumentException("SRID must be greater than 0.", nameof(srid));

        geometry.SRID = srid;

        return geometry;
    }

    private const string GmlNamespace = "http://www.opengis.net/gml/3.2";

    public static string ConvertToGml(this Geometry geometry)
    {
        return geometry.ConvertToGml(true);
    }

    /// <summary>
    /// Converts the geometry to its GML 3.2 representation.
    /// </summary>
    /// <param name="geometry">The geometry to convert.</param>
    /// <param name="useHttpsSchema">Whether the srsName should use the https schema.</param>
    /// <param name="coordinatePrecision">
    /// The number of decimals to use for the coordinate values. When <c>null</c> (the default) the precision is derived
    /// from the geometry type (2 decimals for a point, 11 for a polygon/multipolygon/linestring); when specified, that
    /// number of decimals is used instead for every coordinate.
    /// </param>
    public static string ConvertToGml(this Geometry geometry, bool useHttpsSchema, int? coordinatePrecision = null)
    {
        if (geometry is null)
            throw new ArgumentNullException(nameof(geometry));

        if (coordinatePrecision is < 0)
            throw new ArgumentOutOfRangeException(nameof(coordinatePrecision), coordinatePrecision, "Coordinate precision must be greater than or equal to 0.");

        if (geometry is not Polygon && geometry is not MultiPolygon && geometry is not LineString && geometry is not Point)
            throw new InvalidOperationException($"Unsupported geometry type: {geometry.GeometryType}. Supported types: Polygon, MultiPolygon, LineString, Point.");

        var builder = new StringBuilder();
        var settings = new XmlWriterSettings {Indent = false, OmitXmlDeclaration = true};

        if (geometry is Polygon polygon)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "Polygon", GmlNamespace);
                WriteSrsName(xmlwriter, geometry, useHttpsSchema);
                WriteRing((polygon.ExteriorRing as LinearRing)!, xmlwriter, coordinatePrecision);
                WriteInteriorRings(polygon.InteriorRings, polygon.NumInteriorRings, xmlwriter, coordinatePrecision);
                xmlwriter.WriteEndElement();
            }
        }
        else if (geometry is MultiPolygon multiPolygon)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "MultiSurface", GmlNamespace);
                WriteSrsName(xmlwriter, geometry, useHttpsSchema);

                foreach (var p in multiPolygon.Geometries.Cast<Polygon>())
                {
                    xmlwriter.WriteStartElement("gml", "surfaceMember", null!);
                    xmlwriter.WriteStartElement("gml", "Polygon", null!);

                    WriteRing((p.ExteriorRing as LinearRing)!, xmlwriter, coordinatePrecision);
                    WriteInteriorRings(p.InteriorRings, p.NumInteriorRings, xmlwriter, coordinatePrecision);

                    xmlwriter.WriteEndElement();
                    xmlwriter.WriteEndElement();
                }

                xmlwriter.WriteEndElement();
            }
        }
        else if (geometry is LineString lineString)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "LineString", GmlNamespace);
                WriteSrsName(xmlwriter, geometry, useHttpsSchema);
                WritePosList(lineString.Coordinates, xmlwriter, coordinatePrecision);
                xmlwriter.WriteEndElement();
            }
        }
        else if (geometry is Point point)
        {
            using (var xmlwriter = XmlWriter.Create(builder, settings))
            {
                xmlwriter.WriteStartElement("gml", "Point", GmlNamespace);
                WriteSrsName(xmlwriter, point, useHttpsSchema);

                xmlwriter.WriteStartElement("gml", "pos", null!);
                xmlwriter.WriteValue(string.Format(Global.GetNfi(), "{0} {1}",
                    FormatCoordinate(point.Coordinate.X, coordinatePrecision, static v => v.ToPointGeometryCoordinateValueFormat()),
                    FormatCoordinate(point.Coordinate.Y, coordinatePrecision, static v => v.ToPointGeometryCoordinateValueFormat())));
                xmlwriter.WriteEndElement();

                xmlwriter.WriteEndElement();
            }
        }

        return builder.ToString();
    }

    private static void WriteSrsName(XmlWriter xmlWriter, Geometry geometry, bool useHttpsSchema)
    {
        switch (geometry.SRID)
        {
            case SystemReferenceId.SridLambert72:
                xmlWriter.WriteAttributeString("srsName", useHttpsSchema ? SystemReferenceId.SrsNameLambert72.Replace("http://", "https://") : SystemReferenceId.SrsNameLambert72);
                break;
            case SystemReferenceId.SridLambert2008:
                xmlWriter.WriteAttributeString("srsName", useHttpsSchema ? SystemReferenceId.SrsNameLambert2008.Replace("http://", "https://") : SystemReferenceId.SrsNameLambert2008);
                break;
            default:
                throw new InvalidOperationException($"Unsupported SRID: {geometry.SRID}.");
        }
    }

    private static void WriteRing(
        LinearRing ring,
        XmlWriter writer,
        int? coordinatePrecision,
        bool isInterior = false)
    {
        writer.WriteStartElement("gml", isInterior ? "interior" : "exterior", GmlNamespace);
        writer.WriteStartElement("gml", "LinearRing", GmlNamespace);

        WritePosList(ring.Coordinates, writer, coordinatePrecision);

        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static void WriteInteriorRings(
        LineString[] rings,
        int numInteriorRings,
        XmlWriter writer,
        int? coordinatePrecision)
    {
        if (numInteriorRings < 1)
        {
            return;
        }

        foreach (var ring in rings)
        {
            WriteRing((ring as LinearRing)!, writer, coordinatePrecision, true);
        }
    }

    private static void WritePosList(
        Coordinate[] coordinates,
        XmlWriter writer,
        int? coordinatePrecision)
    {
        writer.WriteStartElement("gml", "posList", GmlNamespace);

        var posListBuilder = new StringBuilder();
        foreach (var coordinate in coordinates)
        {
            posListBuilder.Append(string.Format(
                Global.GetNfi(),
                "{0} {1} ",
                FormatCoordinate(coordinate.X, coordinatePrecision, static v => v.ToPolygonGeometryCoordinateValueFormat()),
                FormatCoordinate(coordinate.Y, coordinatePrecision, static v => v.ToPolygonGeometryCoordinateValueFormat())));
        }

        //remove last space
        if (posListBuilder.Length > 0)
            posListBuilder.Length--;
        writer.WriteValue(posListBuilder.ToString());

        writer.WriteEndElement();
    }

    // Formats a coordinate value: the explicitly requested number of decimals when a coordinate precision is given,
    // otherwise the geometry-type based default format.
    private static string FormatCoordinate(double value, int? coordinatePrecision, Func<double, string> defaultFormat)
    {
        return coordinatePrecision.HasValue
            ? value.ToString("F" + coordinatePrecision.Value.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
            : defaultFormat(value);
    }
}
