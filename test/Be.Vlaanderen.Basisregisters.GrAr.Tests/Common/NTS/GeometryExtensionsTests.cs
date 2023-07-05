namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.NTS
{
    using FluentAssertions;
    using GrAr.Common.NetTopology;
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
    }
}
