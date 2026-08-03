namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Edit.ValidatorTests
{
    using System;
    using Common.NTS;
    using FluentAssertions;
    using GrAr.Common.NetTopology;
    using GrAr.Edit.Validators;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using NetTopologySuite.Operation.Valid;
    using Xunit;

    public class GmlPolygonValidatorTests
    {
        private const string GmlNamespace = "xmlns:gml=\"http://www.opengis.net/gml/3.2\"";

        private const string PosList =
            "140284.15277253836 186724.74131567031 " +
            "140291.06016454101 186726.38355567306 " +
            "140288.22675654292 186738.25798767805 " +
            "140281.19098053873 186736.57913967967 " +
            "140284.15277253836 186724.74131567031";

        private const string SelfTouchingRingPosList =
            "30359.924344554543 197007.54170677811 30359.446008555591 197010.21338678151 30371.943992562592 197013.23297078162 30373.701176568866 197006.42113077641 30363.939512558281 197004.00340277702 30364.205112561584 197002.85997877643 30357.719608552754 197001.36161077395 30356.638264551759 197006.90023477748 30359.924344554543 197007.54170677811 30360.468344554305 197004.48564277589 30362.562808558345 197004.85844277591 30362.018680557609 197007.91457077861 30359.924344554543 197007.54170677811";

        private readonly GMLReader _gmlReader;

        public GmlPolygonValidatorTests()
        {
            _gmlReader = new GMLReader(new GeometryFactory(new PrecisionModel(PrecisionModels.Floating)));
        }

        private static string PolygonGml(string srsName, string posList = PosList) =>
            $"<gml:Polygon srsName=\"{srsName}\" {GmlNamespace}>" +
            $"<gml:exterior><gml:LinearRing><gml:posList>{posList}</gml:posList></gml:LinearRing></gml:exterior>" +
            "</gml:Polygon>";
        private static Func<Geometry, IsValidOp> SelfTouchingRingFormingHoleIsValid =>
            geometry => new IsValidOp(geometry) { SelfTouchingRingFormingHoleValid = true };

        #region IsValid — obsolete, Lambert 72 only

        // These deliberately exercise the obsolete overloads, so that replacing them with IsValidPolygon
        // stays a conscious decision rather than a silent behaviour change.
#pragma warning disable CS0618

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

        /// <summary>
        /// The obsolete overloads match the srsName against a hardcoded Lambert 72 with an https scheme,
        /// which is the limitation <see cref="GmlPolygonValidator.IsValidPolygon(string)"/> lifts.
        /// </summary>
        [Theory]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/31370", true)]
        [InlineData("http://www.opengis.net/def/crs/EPSG/0/31370", false)]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/3812", false)]
        [InlineData("http://www.opengis.net/def/crs/EPSG/0/3812", false)]
        public void GivenSrsName_ThenOnlyHttpsLambert72IsValid(string srsName, bool expectedResult)
        {
            GmlPolygonValidator.IsValid(PolygonGml(srsName), _gmlReader).Should().Be(expectedResult);
        }

#pragma warning restore CS0618

        #endregion

        #region IsValidPolygon — Lambert 72 and Lambert 2008

        [Theory]
        [InlineData(SystemReferenceId.SrsNameLambert72, SystemReferenceId.SridLambert72)]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/31370", SystemReferenceId.SridLambert72)]
        [InlineData(SystemReferenceId.SrsNameLambert2008, SystemReferenceId.SridLambert2008)]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/3812", SystemReferenceId.SridLambert2008)]
        public void GivenSupportedSrsName_ThenPolygonIsValidWithMatchingSrid(string srsName, int expectedSrid)
        {
            GmlPolygonValidator.IsValidPolygon(PolygonGml(srsName), null, out var polygon).Should().BeTrue();

            polygon.Should().NotBeNull();
            polygon!.SRID.Should().Be(expectedSrid);
            polygon.NumPoints.Should().Be(5);
            polygon.ExteriorRing.Coordinates[0].X.Should().Be(140284.15277253836);
            polygon.ExteriorRing.Coordinates[0].Y.Should().Be(186724.74131567031);
        }

        [Fact]
        public void GivenSrsNameWithDifferentCasing_ThenPolygonIsValid()
        {
            var gml = PolygonGml("HTTPS://WWW.OPENGIS.NET/DEF/CRS/EPSG/0/31370");

            GmlPolygonValidator.IsValidPolygon(gml, null, out var polygon).Should().BeTrue();

            polygon.Should().NotBeNull();
            polygon!.SRID.Should().Be(SystemReferenceId.SridLambert72);
        }

        [Fact]
        public void GivenGmlWithXmlDeclaration_ThenPolygonIsValid()
        {
            var gml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + PolygonGml(SystemReferenceId.SrsNameLambert72);

            GmlPolygonValidator.IsValidPolygon(gml).Should().BeTrue();
        }

        [Fact]
        public void GivenPolygonWithInteriorRing_ThenPolygonIsValid()
        {
            var gml =
                $"<gml:Polygon srsName=\"{SystemReferenceId.SrsNameLambert2008}\" {GmlNamespace}>" +
                "<gml:exterior><gml:LinearRing><gml:posList>0 0 100 0 100 100 0 100 0 0</gml:posList></gml:LinearRing></gml:exterior>" +
                "<gml:interior><gml:LinearRing><gml:posList>10 10 20 10 20 20 10 20 10 10</gml:posList></gml:LinearRing></gml:interior>" +
                "</gml:Polygon>";

            GmlPolygonValidator.IsValidPolygon(gml, null, out var polygon).Should().BeTrue();

            polygon.Should().NotBeNull();
            polygon!.NumInteriorRings.Should().Be(1);
            polygon.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        }

        [Theory]
        // No gml
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        // Not xml
        [InlineData("12345")]
        [InlineData("12345 srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"")]
        // Missing gml namespace
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>")]
        // Wrong gml version
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.1\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>")]
        // Missing srsName
        [InlineData("<gml:Polygon xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>")]
        // Unsupported srsName
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/4326\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>4.35 50.85 4.36 50.85 4.36 50.86 4.35 50.85</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>")]
        // Not a polygon
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>")]
        public void GivenInvalidGml_ThenPolygonIsNotValidAndPolygonIsNull(string? gml)
        {
            GmlPolygonValidator.IsValidPolygon(gml, null, out var polygon).Should().BeFalse();

            polygon.Should().BeNull();
        }

        [Fact]
        public void GivenMultiSurface_ThenPolygonIsNotValid()
        {
            // Only a single gml:Polygon is accepted, a gml:MultiSurface reads as a MultiPolygon.
            GmlPolygonValidator.IsValidPolygon(ConstantGmls.ValidGmlMultiPolygon).Should().BeFalse();
        }

        [Fact]
        public void GivenSrsNameOnANestedElementOnly_ThenPolygonIsNotValid()
        {
            // Only the srsName of the outermost element is taken into account.
            var gml =
                $"<gml:Polygon {GmlNamespace}>" +
                $"<gml:exterior><gml:LinearRing srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\"><gml:posList>{PosList}</gml:posList></gml:LinearRing></gml:exterior>" +
                "</gml:Polygon>";

            GmlPolygonValidator.IsValidPolygon(gml).Should().BeFalse();
        }

        [Fact]
        public void GivenTruncatedGml_ThenPolygonIsNotValid()
        {
            var gml =
                $"<gml:Polygon srsName=\"{SystemReferenceId.SrsNameLambert72}\" {GmlNamespace}>" +
                $"<gml:exterior><gml:LinearRing><gml:posList>{PosList}</gml:posList></gml:LinearRing></gml:exterior>";

            GmlPolygonValidator.IsValidPolygon(gml).Should().BeFalse();
        }

        [Fact]
        public void GivenPolygonWithPointsThatDoNotFormClosedLinestring_ThenPolygonIsNotValid()
        {
            // The GMLReader throws an ArgumentException on a ring that is not closed, which is caught.
            var posList =
                "140204.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186838.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031";

            GmlPolygonValidator
                .IsValidPolygon(PolygonGml(SystemReferenceId.SrsNameLambert2008, posList), null, out var polygon)
                .Should().BeFalse();

            polygon.Should().BeNull();
        }

        [Fact]
        public void GivenPolygonWithoutExterior_ThenPolygonIsValidAndEmpty()
        {
            // Documents current behaviour: a gml:Polygon without a gml:exterior reads as an empty polygon,
            // and an empty geometry is valid.
            var gml = $"<gml:Polygon srsName=\"{SystemReferenceId.SrsNameLambert72}\" {GmlNamespace}></gml:Polygon>";

            GmlPolygonValidator.IsValidPolygon(gml, null, out var polygon).Should().BeTrue();

            polygon.Should().NotBeNull();
            polygon!.IsEmpty.Should().BeTrue();
        }

        [Theory]
        [InlineData(SystemReferenceId.SrsNameLambert72)]
        [InlineData(SystemReferenceId.SrsNameLambert2008)]
        public void GivenSelfTouchingRing_ThenPolygonIsNotValidByDefault(string srsName)
        {
            GmlPolygonValidator
                .IsValidPolygon(PolygonGml(srsName, SelfTouchingRingPosList), null, out var polygon)
                .Should().BeFalse();

            polygon.Should().BeNull();
        }

        [Theory]
        [InlineData(SystemReferenceId.SrsNameLambert72, SystemReferenceId.SridLambert72)]
        [InlineData(SystemReferenceId.SrsNameLambert2008, SystemReferenceId.SridLambert2008)]
        public void GivenSelfTouchingRingAndMatchingValidOp_ThenPolygonIsValid(string srsName, int expectedSrid)
        {
            GmlPolygonValidator
                .IsValidPolygon(
                    PolygonGml(srsName, SelfTouchingRingPosList),
                    SelfTouchingRingFormingHoleIsValid,
                    out var polygon)
                .Should().BeTrue();

            polygon.Should().NotBeNull();
            polygon!.SRID.Should().Be(expectedSrid);
        }

        /// <summary>
        /// The <c>isValidOpFactory</c> gets the polygon as read in its own reference system, not one reprojected
        /// to a default.
        /// </summary>
        [Theory]
        [InlineData(SystemReferenceId.SrsNameLambert72, SystemReferenceId.SridLambert72)]
        [InlineData(SystemReferenceId.SrsNameLambert2008, SystemReferenceId.SridLambert2008)]
        public void GivenValidOpFactory_ThenItReceivesThePolygonInItsOwnReferenceSystem(string srsName, int expectedSrid)
        {
            Geometry? received = null;

            GmlPolygonValidator
                .IsValidPolygon(PolygonGml(srsName), geometry =>
                {
                    received = geometry;
                    return new IsValidOp(geometry);
                })
                .Should().BeTrue();

            received.Should().NotBeNull();
            received!.SRID.Should().Be(expectedSrid);
        }

        #endregion
    }
}
