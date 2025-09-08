namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System;
    using System.Xml;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using NetTopologySuite.Operation.Valid;

    public static class GmlPolygonValidator
    {
        public static bool IsValid(string? gml, GMLReader gmlReader)
        {
            return IsValid(gml, gmlReader, null);
        }

        public static bool IsValid(
            string? gml,
            GMLReader gmlReader,
            Func<Geometry, IsValidOp>? isValidOpFactory)
        {
            return IsValid(gml, gmlReader, isValidOpFactory, out _);
        }

        public static bool IsValid(
            string? gml,
            GMLReader gmlReader,
            Func<Geometry, IsValidOp>? isValidOpFactory,
            out Polygon? polygon)
        {
            polygon = null;
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlConstants.GmlVersionAttribute) || !gml.Contains(GmlConstants.SrsNameAttribute))
            {
                return false;
            }

            try
            {
                var geometry = gmlReader.Read(gml);

                if (geometry is Polygon && (isValidOpFactory is null ? geometry.IsValid : isValidOpFactory(geometry).IsValid))
                {
                    polygon = (Polygon)geometry;
                    return true;
                }

                return false;
            }
            catch (XmlException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
