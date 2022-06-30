namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Edit.ValidatorTests
{
    using FluentAssertions;
    using GrAr.Edit.Validators;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using Xunit;

    public class GmlPointValidatorTests
    {
        private readonly GMLReader _gmlReader;

        public GmlPointValidatorTests()
        {
            _gmlReader = new GMLReader(new GeometryFactory(new PrecisionModel(PrecisionModels.Floating)));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("12345", false)]
        [InlineData("12345 srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"", false)]
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>", true)]
        [InlineData("<gml:Point xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>", false)]
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>", false)]
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>", false)]
        public void GivenGml_ThenExpectedResult(string gml, bool expectedResult)
        {
            GmlPointValidator.IsValid(gml, _gmlReader).Should().Be(expectedResult);
        }
    }
}
