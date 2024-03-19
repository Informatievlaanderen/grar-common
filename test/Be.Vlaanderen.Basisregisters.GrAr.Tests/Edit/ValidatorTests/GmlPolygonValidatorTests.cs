namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Edit.ValidatorTests
{
    using FluentAssertions;
    using GrAr.Edit.Validators;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using Xunit;

    public class GmlPolygonValidatorTests
    {
        private readonly GMLReader _gmlReader;

        public GmlPolygonValidatorTests()
        {
            _gmlReader = new GMLReader(new GeometryFactory(new PrecisionModel(PrecisionModels.Floating)));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("12345", false)]
        [InlineData("12345 srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"", false)]
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>", false)]
        [InlineData("<gml:Polygon xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>", false)]
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>", false)]
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>", true)]
        // This is a selftouchingring, same as test below. By default INVALID!
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>30359.924344554543 197007.54170677811 30359.446008555591 197010.21338678151 30371.943992562592 197013.23297078162 30373.701176568866 197006.42113077641 30363.939512558281 197004.00340277702 30364.205112561584 197002.85997877643 30357.719608552754 197001.36161077395 30356.638264551759 197006.90023477748 30359.924344554543 197007.54170677811 30360.468344554305 197004.48564277589 30362.562808558345 197004.85844277591 30362.018680557609 197007.91457077861 30359.924344554543 197007.54170677811</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>", false)]
        public void GivenGml_ThenExpectedResult(string gml, bool expectedResult)
        {
            GmlPolygonValidator.IsValid(gml, _gmlReader).Should().Be(expectedResult);
        }

        [Fact]
        public void GivenGmlSelfTouchingIntersectionValidOp_ThenGmlIsValid()
        {
            var validOp = (Geometry geometry) =>
                new NetTopologySuite.Operation.Valid.IsValidOp(geometry)
                {
                    IsSelfTouchingRingFormingHoleValid = true,
                    SelfTouchingRingFormingHoleValid = true
                };

            var gml =
                "<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>30359.924344554543 197007.54170677811 30359.446008555591 197010.21338678151 30371.943992562592 197013.23297078162 30373.701176568866 197006.42113077641 30363.939512558281 197004.00340277702 30364.205112561584 197002.85997877643 30357.719608552754 197001.36161077395 30356.638264551759 197006.90023477748 30359.924344554543 197007.54170677811 30360.468344554305 197004.48564277589 30362.562808558345 197004.85844277591 30362.018680557609 197007.91457077861 30359.924344554543 197007.54170677811</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>";

            GmlPolygonValidator.IsValid(gml, _gmlReader, validOp).Should().BeTrue();
        }

        [Fact]
        public void GivenPolygonWithPointsThatDoNotFormClosedLinestring_ThenGmlIsNotValid()
        {
            var gml =
                "<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140204.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186838.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>";

            GmlPolygonValidator.IsValid(gml, _gmlReader).Should().BeFalse();
        }
    }
}
