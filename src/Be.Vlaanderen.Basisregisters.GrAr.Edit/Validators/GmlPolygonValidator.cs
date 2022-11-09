namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System;
    using System.Xml;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;

    public static class GmlPolygonValidator
    {
        public static bool IsValid(string? gml, GMLReader gmlReader)
        {
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlConstants.GmlVersionAttribute) || !gml.Contains(GmlConstants.SrsNameAttribute))
            {
                return false;
            }

            try
            {
                var geometry = gmlReader.Read(gml);

                if (geometry is null or not Polygon)
                {
                    return false;
                }

                return geometry.IsValid;
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
