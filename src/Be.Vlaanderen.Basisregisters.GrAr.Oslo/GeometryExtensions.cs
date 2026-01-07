namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo
{
    using System;
    using System.Text;
    using System.Xml;
    using Common.SpatialTools.GeometryCoordinates;
    using Legacy.SpatialTools;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.Utilities;
    using LinearRing = NetTopologySuite.Geometries.LinearRing;
    using Polygon = NetTopologySuite.Geometries.Polygon;

    public static class GeometryExtensions
    {
        public static GmlJsonPoint ToGmlJsonPoint(this Geometry geometry)
        {
            if (geometry.SRID == 0)
                throw new InvalidOperationException("SRID must be set on geometry before converting to GML.");

            var builder = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true };
            using (var xmlWriter = XmlWriter.Create(builder, settings))
            {
                xmlWriter.WriteStartElement("gml", "Point", "http://www.opengis.net/gml/3.2");
                xmlWriter.WriteAttributeString("srsName", $"https://www.opengis.net/def/crs/EPSG/0/{geometry.SRID}");
                WritePoint(geometry.Coordinate, xmlWriter);
                xmlWriter.WriteEndElement();
            }

            return new GmlJsonPoint(builder.ToString());
        }

        private static void WritePoint(Coordinate coordinate, XmlWriter writer)
        {
            writer.WriteStartElement("gml", "pos", "http://www.opengis.net/gml/3.2");
            writer.WriteValue(string.Format(Global.GetNfi(), "{0} {1}",
                coordinate.X.ToPointGeometryCoordinateValueFormat(), coordinate.Y.ToPointGeometryCoordinateValueFormat()));
            writer.WriteEndElement();
        }

        public static GmlJsonPolygon? ToGmlJsonPolygon(this Geometry geometry)
        {
            var builder = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true };

            var polygon = geometry as Polygon;
            if (polygon == null)
                return null;

            if (polygon.SRID == 0)
                throw new InvalidOperationException("SRID must be set on polygon before converting to GML.");

            using (var xmlWriter = XmlWriter.Create(builder, settings))
            {
                xmlWriter.WriteStartElement("gml", "Polygon", "http://www.opengis.net/gml/3.2");
                xmlWriter.WriteAttributeString("srsName", $"https://www.opengis.net/def/crs/EPSG/0/{polygon.SRID}");
                WriteRing((polygon.ExteriorRing as LinearRing)!, xmlWriter);
                WriteInteriorRings(polygon.InteriorRings, polygon.NumInteriorRings, xmlWriter);
                xmlWriter.WriteEndElement();
            }
            return new GmlJsonPolygon(builder.ToString());
        }

        private static void WriteRing(LinearRing ring, XmlWriter writer, bool isInterior = false)
        {
            writer.WriteStartElement("gml", isInterior ? "interior" : "exterior", "http://www.opengis.net/gml/3.2");
            writer.WriteStartElement("gml", "LinearRing", "http://www.opengis.net/gml/3.2");
            writer.WriteStartElement("gml", "posList", "http://www.opengis.net/gml/3.2");

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

        private static void WriteInteriorRings(LineString[] rings, int numInteriorRings, XmlWriter writer)
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
}
