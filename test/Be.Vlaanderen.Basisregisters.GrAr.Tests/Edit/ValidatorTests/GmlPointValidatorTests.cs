namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Edit.ValidatorTests
{
    using System;
    using FluentAssertions;
    using GrAr.Common.NetTopology;
    using GrAr.Edit.Validators;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO.GML2;
    using Xunit;

    public class GmlPointValidatorTests
    {
        private const string GmlNamespace = "xmlns:gml=\"http://www.opengis.net/gml/3.2\"";

        private readonly GMLReader _gmlReader;

        public GmlPointValidatorTests()
        {
            _gmlReader = new GMLReader(new GeometryFactory(new PrecisionModel(PrecisionModels.Floating)));
        }

        private static string PointGml(string srsName) =>
            $"<gml:Point srsName=\"{srsName}\" {GmlNamespace}><gml:pos>188473.52 193390.22</gml:pos></gml:Point>";

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

        [Theory]
        [InlineData(SystemReferenceId.SrsNameLambert72, SystemReferenceId.SridLambert72)]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/31370", SystemReferenceId.SridLambert72)]
        [InlineData(SystemReferenceId.SrsNameLambert2008, SystemReferenceId.SridLambert2008)]
        [InlineData("https://www.opengis.net/def/crs/EPSG/0/3812", SystemReferenceId.SridLambert2008)]
        public void GivenSupportedSrsName_ThenPointIsValidWithMatchingSrid(string srsName, int expectedSrid)
        {
            GmlPointValidator.IsValidPoint(PointGml(srsName), out var point).Should().BeTrue();

            point.Should().NotBeNull();
            point!.SRID.Should().Be(expectedSrid);
            point.X.Should().Be(188473.52);
            point.Y.Should().Be(193390.22);
        }

        [Fact]
        public void GivenSrsNameWithDifferentCasing_ThenPointIsValid()
        {
            var gml = PointGml("HTTPS://WWW.OPENGIS.NET/DEF/CRS/EPSG/0/31370");

            GmlPointValidator.IsValidPoint(gml, out var point).Should().BeTrue();

            point.Should().NotBeNull();
            point!.SRID.Should().Be(SystemReferenceId.SridLambert72);
        }

        [Fact]
        public void GivenGmlWithXmlDeclaration_ThenPointIsValid()
        {
            var gml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + PointGml(SystemReferenceId.SrsNameLambert72);

            GmlPointValidator.IsValidPoint(gml).Should().BeTrue();
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
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>")]
        // Wrong gml version
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.1\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>")]
        // Missing srsName
        [InlineData("<gml:Point xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>188473.52 193390.22</gml:pos></gml:Point>")]
        // Unsupported srsName
        [InlineData("<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/4326\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:pos>4.35 50.85</gml:pos></gml:Point>")]
        // Not a point
        [InlineData("<gml:Polygon srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"><gml:exterior><gml:LinearRing><gml:posList>140284.15277253836 186724.74131567031 140291.06016454101 186726.38355567306 140288.22675654292 186738.25798767805 140281.19098053873 186736.57913967967 140284.15277253836 186724.74131567031</gml:posList></gml:LinearRing></gml:exterior></gml:Polygon>")]
        public void GivenInvalidGml_ThenPointIsNotValidAndPointIsNull(string? gml)
        {
            GmlPointValidator.IsValidPoint(gml, out var point).Should().BeFalse();

            point.Should().BeNull();
        }

        [Fact]
        public void GivenSrsNameOnANestedElementOnly_ThenPointIsNotValid()
        {
            // Only the srsName of the outermost element is taken into account.
            var gml =
                "<gml:Point xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:pos srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\">188473.52 193390.22</gml:pos>" +
                "</gml:Point>";

            GmlPointValidator.IsValidPoint(gml).Should().BeFalse();
        }

        [Fact]
        public void GivenTruncatedGml_ThenPointIsValid()
        {
            // Documents current behaviour: the GMLReader returns the point it has already read
            // when the document ends prematurely, so truncated gml is not rejected.
            var gml =
                "<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\">" +
                "<gml:pos>188473.52 193390.22</gml:pos>";

            GmlPointValidator.IsValidPoint(gml).Should().BeTrue();
        }

        [Fact]
        public void GivenGmlPointWithoutPosition_ThenItThrows()
        {
            // Documents current behaviour: only XmlException is caught, but the GMLReader throws
            // an ArgumentException when a gml:Point has no gml:pos.
            var gml =
                "<gml:Point srsName=\"https://www.opengis.net/def/crs/EPSG/0/31370\" xmlns:gml=\"http://www.opengis.net/gml/3.2\"></gml:Point>";

            var act = () => GmlPointValidator.IsValidPoint(gml);

            act.Should().Throw<ArgumentException>();
        }
    }
}
