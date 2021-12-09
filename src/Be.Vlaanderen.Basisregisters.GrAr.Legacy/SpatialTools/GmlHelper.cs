namespace Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    public static class GmlHelper
    {
        public static List<PointGeometryCoordinateValue> MapToPointGeometryCoordinateValues(double[] pointGeometryCoordinates)
            => pointGeometryCoordinates.Select(i => new PointGeometryCoordinateValue(i)).ToList();

        public static GmlPoint ToGmlPoint(double[] pointGeometryCoordinates)
        {
            var coordinates = MapToPointGeometryCoordinateValues(pointGeometryCoordinates);
            var gmlPosValue = string.Join(' ', coordinates.Select(i => i.ToString()));
            return new GmlPoint {Pos = gmlPosValue};
        }

        public static string ToGmlPointString(double[] pointGeometryCoordinates)
        {
            var coordinates = MapToPointGeometryCoordinateValues(pointGeometryCoordinates);
            var gmlPosValue = string.Join(' ', coordinates.Select(i => i.ToString()));
            return $"<gml:Point srsName='https://www.opengis.net/def/crs/EPSG/0/31370'><gml:pos>{gmlPosValue}</gml:pos></gml:Point>";
        }

        public static double[] ParseGmlPointString(string gml)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gml))
                    throw new Exception();

                var container = $"<container xmlns:gml='http://schemas.opengis.net/gml/3.2.1'>{gml}</container>";
                var value = XElement.Parse(container, LoadOptions.PreserveWhitespace).Value.Split(" ");
                var coordinates = value
                    .Select(i => double.Parse(i, CultureInfo.InvariantCulture))
                    .ToArray();
                return coordinates;
            }
            catch (Exception e)
            {
                throw new Exception("Invalid value", e);
            }
        }
    }
}
