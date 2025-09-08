namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System.Xml;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;

    public static class GmlPointValidator
    {
        public static bool IsValid(string? gml, GMLReader gmlReader)
        {
            return IsValid(gml, gmlReader, out _);
        }

        public static bool IsValid(string? gml, GMLReader gmlReader, out Point? point)
        {
            point = null;
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlConstants.GmlVersionAttribute) || !gml.Contains(GmlConstants.SrsNameAttribute))
            {
                return false;
            }

            try
            {
                var geometry = gmlReader.Read(gml);

                if (geometry is Point && geometry.IsValid)
                {
                    point = (Point)geometry;
                    return true;
                }

                return false;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
