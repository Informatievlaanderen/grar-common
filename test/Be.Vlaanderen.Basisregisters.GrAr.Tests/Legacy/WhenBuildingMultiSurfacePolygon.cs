namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Legacy
{
    using Common.NTS;
    using FluentAssertions;
    using GrAr.Legacy.SpatialTools;
    using NetTopologySuite;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.Geometries.Implementation;
    using NetTopologySuite.IO;
    using NetTopologySuite.IO.GML2;
    using Xunit;

    // Example output of GmlMultiSurfaceBuilder
    //
    //<MeervoudigeOppervlakte xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
    //  <multiSurface>
    //    <surfaceMember>
    //      <polygon>
    //        <exterior>
    //          <LinearRing>
    //            <posList>12.345 67.891 23.456 78.912 34.567 89.123 45.678 90.123 56.789 12.345</posList>
    //          </LinearRing>
    //        </exterior>
    //      </polygon>
    //    </surfaceMember>
    //    <surfaceMember>
    //      <polygon>
    //        <exterior>
    //          <LinearRing>
    //            <posList>98.765 43.210 87.654 32.109 76.543 21.098 65.432 10.987 54.321 98.765</posList>
    //          </LinearRing>
    //        </exterior>
    //      </polygon>
    //    </surfaceMember>
    //  </multiSurface>
    //</MeervoudigeOppervlakte>
    public class WhenBuildingMultiSurfacePolygon
    {
        private const int SridLambert72 = 31370;

        private readonly GmlMultiSurface _testResult;

        public WhenBuildingMultiSurfacePolygon()
        {
            var wkbReader = new WKBReader(
                new NtsGeometryServices(
                    new DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY),
                    new PrecisionModel(PrecisionModels.Floating),
                    SridLambert72));

            var e = new GMLReader().Read(ConstantGmls.ValidGmlMultiPolygon);
            _testResult = GmlMultiSurfaceBuilder.Build(e.AsBinary(), wkbReader);
        }

        [Fact]
        public void ThenSurfaceMembersShouldNotBeEmpty() => _testResult.Should().NotBeEmpty();

        [Fact]
        public void ThenSurfaceMemberShouldHaveCountTwo() => _testResult.Should().HaveCount(2);

        [Fact]
        public void ThenSurfaceMember1ShouldHaveInteriorExteriorRings()
        {
            var polygon = _testResult[0].Polygon;
            polygon.Interior.Should().NotBeEmpty();
            polygon.Exterior.Should().NotBeNull();

            foreach (var interior in polygon.Interior)
            {
                interior.LinearRing.PosList.Should().NotBeNullOrEmpty();
            }

            polygon.Exterior.LinearRing.PosList.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ThenSurfaceMember2ShouldOnlyHaveExteriorRing()
        {
            var polygon = _testResult[1].Polygon;
            polygon.Interior.Should().BeNullOrEmpty();
            polygon.Exterior.Should().NotBeNull();
            polygon.Exterior.LinearRing.PosList.Should().NotBeNullOrEmpty();
        }
    }
}
