namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.NTS
{
    using System;
    using FluentAssertions;
    using GrAr.Common.NetTopology;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using Xunit;

    public class GeometryExtensionsTests
    {
        [Fact]
        public void WhenConvertLineStringToGmlWithCoordinatePrecision_ThenCoordinatesUseThatPrecision()
        {
            var geometry = new LineString(
            [
                new Coordinate(10, 20),
                new Coordinate(30, 40)
            ]);
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml(true, 2);

            resultingGml.Should().Be(
                "<gml:LineString srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:posList>10.00 20.00 30.00 40.00</gml:posList>" +
                "</gml:LineString>");
        }

        [Fact]
        public void WhenConvertLineStringToGmlWithCoordinatePrecisionZero_ThenCoordinatesHaveNoDecimals()
        {
            var geometry = new LineString(
            [
                new Coordinate(10, 20),
                new Coordinate(30, 40)
            ]);
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml(true, 0);

            resultingGml.Should().Be(
                "<gml:LineString srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:posList>10 20 30 40</gml:posList>" +
                "</gml:LineString>");
        }

        [Fact]
        public void WhenConvertPointToGmlWithCoordinatePrecision_ThenCoordinatesUseThatPrecision()
        {
            var geometry = new Point(new Coordinate(1.123456, 2.5)) { SRID = SystemReferenceId.SridLambert72 };

            var resultingGml = geometry.ConvertToGml(true, 5);

            resultingGml.Should().Be(
                "<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:pos>1.12346 2.50000</gml:pos>" +
                "</gml:Point>");
        }

        [Fact]
        public void WhenConvertPolygonToGmlWithCoordinatePrecision_ThenCoordinatesUseThatPrecision()
        {
            var geometry = new Polygon(new LinearRing(
            [
                new Coordinate(0, 0),
                new Coordinate(0, 10),
                new Coordinate(10, 10),
                new Coordinate(10, 0),
                new Coordinate(0, 0)
            ]));
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml(true, 1);

            resultingGml.Should().Be(
                "<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:exterior><gml:LinearRing>" +
                "<gml:posList>0.0 0.0 0.0 10.0 10.0 10.0 10.0 0.0 0.0 0.0</gml:posList>" +
                "</gml:LinearRing></gml:exterior>" +
                "</gml:Polygon>");
        }

        [Fact]
        public void WhenConvertToGmlWithNullCoordinatePrecision_ThenUsesGeometryTypeBasedPrecision()
        {
            var geometry = new LineString(
            [
                new Coordinate(10, 20),
                new Coordinate(30, 40)
            ]);
            geometry.SRID = SystemReferenceId.SridLambert72;

            geometry.ConvertToGml(true, null).Should().Be(geometry.ConvertToGml(true));
        }

        [Fact]
        public void WhenConvertToGmlWithNegativeCoordinatePrecision_ThenThrows()
        {
            var geometry = new Point(new Coordinate(1, 2)) { SRID = SystemReferenceId.SridLambert72 };

            var act = () => geometry.ConvertToGml(true, -1);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void WhenConvertPolygonToGml_ThenProduceValidXml()
        {
            var geometry = new GMLReader().Read(ConstantGmls.ValidGmlPolygon);

            var resultingGml = geometry.ConvertToGml();

            ConstantGmls.ValidGmlPolygon.Should().Be(resultingGml);
        }

        [Fact]
        public void WhenConvertPolygonToGmlWithHttpSchema_ThenProduceValidXml()
        {
            var geometry = new GMLReader().Read(ConstantGmls.ValidGmlPolygon);

            var resultingGml = geometry.ConvertToGml(false);

            ConstantGmls.ValidGmlPolygonHttp.Should().Be(resultingGml);
        }

        [Fact]
        public void WhenConvertPointToGml_ThenProduceValidXml()
        {
            var geometry = new GMLReader().Read(ConstantGmls.ValidGmlPoint);

            var resultingGml = geometry.ConvertToGml();

            ConstantGmls.ValidGmlPoint.Should().Be(resultingGml);
        }

        [Fact]
        public void WhenConvertMultiPolygonToGml_ThenProduceValidXml()
        {
           var geometry = new GMLReader().Read(ConstantGmls.ValidGmlMultiPolygon);

            var resultingGml = geometry.ConvertToGml();

            ConstantGmls.ValidGmlMultiPolygon.Should().BeEquivalentTo(resultingGml);
        }

        [Fact]
        public void WhenConvertLineStringToGml_ThenProduceValidXml()
        {
            var geometry = new LineString(
            [
                new Coordinate(10, 20),
                new Coordinate(30, 40)
            ]);
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml();

            resultingGml.Should().Be(
                "<gml:LineString srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:posList>10.00000000000 20.00000000000 30.00000000000 40.00000000000</gml:posList>" +
                "</gml:LineString>");
        }

        [Fact]
        public void WhenConvertLineStringWithoutCoordinatesToGml_ThenProduceValidXml()
        {
            var geometry = GeometryFactory.Default.CreateLineString();
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml();

            resultingGml.Should().Be(
                "<gml:LineString srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:posList></gml:posList></gml:LineString>");
        }
    }
}
