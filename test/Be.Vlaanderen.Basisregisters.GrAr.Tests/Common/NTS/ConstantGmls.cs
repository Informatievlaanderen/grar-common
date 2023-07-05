namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.NTS
{
    public class ConstantGmls
    {
        public const string ValidGmlPolygon =
            "<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
            "<gml:exterior>" +
            "<gml:LinearRing>" +
            "<gml:posList>" +
            "140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031" +
            "</gml:posList>" +
            "</gml:LinearRing>" +
            "</gml:exterior>" +
            "</gml:Polygon>";

        public const string ValidGmlMultiPolygon =
            "<gml:MultiSurface srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
            "<gml:surfaceMember>" +
            "<gml:Polygon>" +
            "<gml:exterior>" +
            "<gml:LinearRing>" +
            "<gml:posList>10.00000000000 10.00000000000 20.00000000000 10.00000000000 20.00000000000 20.00000000000 10.00000000000 20.00000000000 10.00000000000 10.00000000000</gml:posList>" +
            "</gml:LinearRing>" +
            "</gml:exterior>" +
            "<gml:interior>" +
            "<gml:LinearRing>" +
            "<gml:posList>12.00000000000 12.00000000000 18.00000000000 12.00000000000 18.00000000000 18.00000000000 12.00000000000 18.00000000000 12.00000000000 12.00000000000</gml:posList>" +
            "</gml:LinearRing>" +
            "</gml:interior>" +
            "</gml:Polygon>" +
            "</gml:surfaceMember>" +
            "<gml:surfaceMember>" +
            "<gml:Polygon>" +
            "<gml:exterior>" +
            "<gml:LinearRing>" +
            "<gml:posList>30.00000000000 30.00000000000 40.00000000000 30.00000000000 40.00000000000 40.00000000000 30.00000000000 40.00000000000 30.00000000000 30.00000000000</gml:posList>" +
            "</gml:LinearRing>" +
            "</gml:exterior>" +
            "</gml:Polygon>" +
            "</gml:surfaceMember>" +
            "</gml:MultiSurface>";

        public const string ValidGmlPoint =
            "<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
            "<gml:pos>103671.37 192046.71</gml:pos>" +
            "</gml:Point>";
    }
}
