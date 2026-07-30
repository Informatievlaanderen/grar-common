namespace Be.Vlaanderen.Basisregisters.GrAr.Edit.Validators
{
    using System.Xml;
    using Common;
    using Common.NetTopology;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;

    public static class GmlPointValidator
    {
        private const string GmlVersionAttributeValue = "http://www.opengis.net/gml/3.2";
        public static bool IsValid(string? gml, GMLReader gmlReader)
        {
            return IsValid(gml, gmlReader, out _);
        }

        public static bool IsValid(string? gml, GMLReader gmlReader, out Point? point)
        {
            point = null;
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlVersionAttributeValue) || !gml.Contains(GmlConstants.SrsNameAttribute))
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

        /// <summary>
        /// Validates a GML point position. Unlike IsValid(string, GMLReader, out Point?)
        /// this accepts both Lambert 72 (EPSG 31370) and Lambert 2008 (EPSG 3812) as srsName.
        /// </summary>
        public static bool IsValidPoint(string? gml, out Point? point)
        {
            point = null;
            // srsName check is moved to TryReadSridGml
            if (string.IsNullOrEmpty(gml) || !gml.Contains(GmlVersionAttributeValue))
            {
                return false;
            }

            if (!gml.TryReadSridGml(out var srid))
            {
                return false;
            }

            try
            {
                var geometry = GmlFactory.CreateGmlReader(srid).Read(gml);

                if (geometry is Point { IsValid: true } gmlPoint)
                {
                    point = gmlPoint;
                    return true;
                }

                return false;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public static bool IsValidPoint(string? gml) => IsValidPoint(gml, out _);
    }
}
