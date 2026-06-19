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
            var geometry = new LineString(Array.Empty<Coordinate>());
            geometry.SRID = SystemReferenceId.SridLambert72;

            var resultingGml = geometry.ConvertToGml();

            resultingGml.Should().Be(
                "<gml:LineString srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:posList></gml:posList></gml:LineString>");
        }
    }
}
