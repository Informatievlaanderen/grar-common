namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System.Xml;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;

    public static class GmlPointValidator
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

                return geometry is Point && geometry.IsValid;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
