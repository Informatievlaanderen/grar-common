namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.CrsTransform;

using System;
using FluentAssertions;
using GrAr.Common.NetTopology;
using GrAr.CrsTransform;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Xunit;

/// <summary>
/// A lightweight value type representing a coordinate pair,
/// with implicit conversion to string for readable test output.
/// </summary>
public readonly record struct CoordinatePair(double X, double Y)
{
    public static implicit operator string(CoordinatePair c) => $"{c.X:F2}, {c.Y:F2}";
    public static implicit operator CoordinatePair((double X, double Y) tuple) => new(tuple.X, tuple.Y);

    public override string ToString() => this;
}

public class LambertTransformationTests
{
    private static readonly GeometryFactory Lambert72Factory =
        new(new PrecisionModel(), SystemReferenceId.SridLambert72);

    private static readonly GeometryFactory Lambert08Factory =
        new(new PrecisionModel(), SystemReferenceId.SridLambert2008);

    private static Point CreateLambert72Point(double x, double y)
    {
        var point = Lambert72Factory.CreatePoint(new Coordinate(x, y));
        point.SRID = SystemReferenceId.SridLambert72;
        return point;
    }

    private static Point CreateLambert08Point(double x, double y)
    {
        var point = Lambert08Factory.CreatePoint(new Coordinate(x, y));
        point.SRID = SystemReferenceId.SridLambert2008;
        return point;
    }

    // Well-known point inside Flanders (Ghent area) in Lambert 72
    private const double FlandersLambert72X = 104000.0;
    private const double FlandersLambert72Y = 194000.0;

    // Approximate equivalent in Lambert 2008
    private const double FlandersLambert08X = 604000.0;
    private const double FlandersLambert08Y = 694000.0;

    #region TransformFromLambert72To08

    [Fact]
    public void WhenTransformingFromLambert72To08_GivenValidPoint_ThenSridIsLambert08()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var result = point.TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
    }

    [Fact]
    public void WhenTransformingFromLambert72To08_GivenValidPoint_ThenCoordinatesAreTransformed()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var result = point.TransformFromLambert72To08();

        CoordinatePair resultCoord = (result.Coordinate.X, result.Coordinate.Y);
        // Coordinates should be in Lambert 2008 range (roughly 500k-760k X, 650k-745k Y)
        result.Coordinate.X.Should().BeInRange(500_000, 760_000, because: $"X should be in Lambert 2008 range, got {resultCoord}");
        result.Coordinate.Y.Should().BeInRange(650_000, 745_000, because: $"Y should be in Lambert 2008 range, got {resultCoord}");
    }

    [Fact]
    public void WhenTransformingFromLambert72To08_GivenPointWithWrongSrid_ThenThrowsArgumentException()
    {
        var point = Lambert08Factory.CreatePoint(new Coordinate(FlandersLambert08X, FlandersLambert08Y));
        point.SRID = SystemReferenceId.SridLambert2008;

        var act = () => point.TransformFromLambert72To08();

        act.Should().Throw<ArgumentException>().WithMessage("*SRID*");
    }

    [Fact]
    public void WhenTransformingFromLambert72To08_GivenPointWithSridMinusOne_ThenDoesNotThrow()
    {
        var point = new GeometryFactory().CreatePoint(new Coordinate(FlandersLambert72X, FlandersLambert72Y));
        point.SRID = -1;

        var act = () => point.TransformFromLambert72To08();

        act.Should().NotThrow();
    }

    #endregion

    #region TransformFromLambert08To72

    [Fact]
    public void WhenTransformingFromLambert08To72_GivenValidPoint_ThenSridIsLambert72()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        var result = point.TransformFromLambert08To72();

        result.SRID.Should().Be(SystemReferenceId.SridLambert72);
    }

    [Fact]
    public void WhenTransformingFromLambert08To72_GivenValidPoint_ThenCoordinatesAreTransformed()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        var result = point.TransformFromLambert08To72();

        CoordinatePair resultCoord = (result.Coordinate.X, result.Coordinate.Y);
        // Coordinates should be in Lambert 72 range (roughly 21k-260k X, 152k-245k Y)
        result.Coordinate.X.Should().BeInRange(21_000, 260_000, because: $"X should be in Lambert 72 range, got {resultCoord}");
        result.Coordinate.Y.Should().BeInRange(152_000, 245_000, because: $"Y should be in Lambert 72 range, got {resultCoord}");
    }

    [Fact]
    public void WhenTransformingFromLambert08To72_GivenPointWithWrongSrid_ThenThrowsArgumentException()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var act = () => point.TransformFromLambert08To72();

        act.Should().Throw<ArgumentException>().WithMessage("*SRID*");
    }

    #endregion

    #region RoundTrip

    [Fact]
    public void WhenTransformingRoundTrip72To08To72_ThenCoordinatesArePreserved()
    {
        var original = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var lambert08 = original.TransformFromLambert72To08();
        var roundTrip = lambert08.TransformFromLambert08To72();

        roundTrip.Coordinate.X.Should().BeApproximately(original.Coordinate.X, 0.01);
        roundTrip.Coordinate.Y.Should().BeApproximately(original.Coordinate.Y, 0.01);
    }

    [Fact]
    public void WhenTransformingRoundTrip08To72To08_ThenCoordinatesArePreserved()
    {
        var original = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        var lambert72 = original.TransformFromLambert08To72();
        var roundTrip = lambert72.TransformFromLambert72To08();

        roundTrip.Coordinate.X.Should().BeApproximately(original.Coordinate.X, 0.01);
        roundTrip.Coordinate.Y.Should().BeApproximately(original.Coordinate.Y, 0.01);
    }

    #endregion

    #region IsLambert72 / IsLambert08

    [Fact]
    public void WhenCheckingIsLambert72_GivenLambert72Point_ThenReturnsTrue()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        point.IsLambert72().Should().BeTrue();
    }

    [Fact]
    public void WhenCheckingIsLambert72_GivenLambert08Point_ThenReturnsFalse()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        point.IsLambert72().Should().BeFalse();
    }

    [Fact]
    public void WhenCheckingIsLambert08_GivenLambert08Point_ThenReturnsTrue()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        point.IsLambert08().Should().BeTrue();
    }

    [Fact]
    public void WhenCheckingIsLambert08_GivenLambert72Point_ThenReturnsFalse()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        point.IsLambert08().Should().BeFalse();
    }

    #endregion

    #region IsInsideFlanders

    [Fact]
    public void WhenCheckingIsInsideFlandersUsingLambert72_GivenPointInsideFlanders_ThenReturnsTrue()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        point.IsInsideFlandersUsingLambert72().Should().BeTrue();
    }

    [Fact]
    public void WhenCheckingIsInsideFlandersUsingLambert72_GivenPointOutsideFlanders_ThenReturnsFalse()
    {
        var point = CreateLambert72Point(0, 0);

        point.IsInsideFlandersUsingLambert72().Should().BeFalse();
    }

    [Fact]
    public void WhenCheckingIsInsideFlandersUsingLambert08_GivenPointInsideFlanders_ThenReturnsTrue()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        point.IsInsideFlandersUsingLambert08().Should().BeTrue();
    }

    [Fact]
    public void WhenCheckingIsInsideFlandersUsingLambert08_GivenPointOutsideFlanders_ThenReturnsFalse()
    {
        var point = CreateLambert08Point(0, 0);

        point.IsInsideFlandersUsingLambert08().Should().BeFalse();
    }

    #endregion

    #region EnsureLambert72

    [Fact]
    public void WhenEnsureLambert72_GivenLambert08PointInsideFlanders_ThenTransformsToLambert72()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        var result = point.EnsureLambert72();

        result.SRID.Should().Be(SystemReferenceId.SridLambert72);
        result.IsInsideFlandersUsingLambert72().Should().BeTrue();
    }

    [Fact]
    public void WhenEnsureLambert72_GivenLambert72PointInsideFlanders_ThenKeepsSridAsLambert72()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var result = point.EnsureLambert72();

        result.SRID.Should().Be(SystemReferenceId.SridLambert72);
    }

    [Fact]
    public void WhenEnsureLambert72_GivenInvalidGeometry_ThenReturnsGeometryUnchanged()
    {
        // Create an invalid (self-intersecting) polygon
        var coords = new[]
        {
            new Coordinate(0, 0),
            new Coordinate(10, 10),
            new Coordinate(0, 10),
            new Coordinate(10, 0),
            new Coordinate(0, 0)
        };
        var polygon = Lambert72Factory.CreatePolygon(coords);
        polygon.SRID = SystemReferenceId.SridLambert72;

        var result = polygon.EnsureLambert72();

        result.Should().BeSameAs(polygon);
    }

    [Fact]
    public void WhenEnsureLambert72_GivenUnsupportedGeometryType_ThenReturnsGeometryUnchanged()
    {
        // GeometryCollection is not in the supported types list
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);
        var collection = Lambert72Factory.CreateGeometryCollection(new Geometry[] { point });
        collection.SRID = SystemReferenceId.SridLambert72;

        var result = collection.EnsureLambert72();

        ((object)result).Should().BeSameAs(collection);
    }

    #endregion

    #region EnsureLambert08

    [Fact]
    public void WhenEnsureLambert08_GivenLambert72PointInsideFlanders_ThenTransformsToLambert08()
    {
        var point = CreateLambert72Point(FlandersLambert72X, FlandersLambert72Y);

        var result = point.EnsureLambert08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.IsInsideFlandersUsingLambert08().Should().BeTrue();
    }

    [Fact]
    public void WhenEnsureLambert08_GivenLambert08PointInsideFlanders_ThenKeepsSridAsLambert08()
    {
        var point = CreateLambert08Point(FlandersLambert08X, FlandersLambert08Y);

        var result = point.EnsureLambert08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
    }

    [Fact]
    public void WhenEnsureLambert08_GivenInvalidGeometry_ThenReturnsGeometryUnchanged()
    {
        var coords = new[]
        {
            new Coordinate(0, 0),
            new Coordinate(10, 10),
            new Coordinate(0, 10),
            new Coordinate(10, 0),
            new Coordinate(0, 0)
        };
        var polygon = Lambert08Factory.CreatePolygon(coords);
        polygon.SRID = SystemReferenceId.SridLambert2008;

        var result = polygon.EnsureLambert08();

        result.Should().BeSameAs(polygon);
    }

    #endregion

    #region Supported Geometry Types

    [Fact]
    public void WhenTransformingPolygon_GivenLambert72Polygon_ThenTransformsToLambert08()
    {
        var wkt = "POLYGON ((100000 190000, 100100 190000, 100100 190100, 100000 190100, 100000 190000))";
        var geometry = new WKTReader(Lambert72Factory).Read(wkt);
        geometry.SRID = SystemReferenceId.SridLambert72;

        var result = geometry.TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.GeometryType.Should().Be("Polygon");
    }

    [Fact]
    public void WhenTransformingLineString_GivenLambert72LineString_ThenTransformsToLambert08()
    {
        var wkt = "LINESTRING (100000 190000, 100100 190100)";
        var geometry = new WKTReader(Lambert72Factory).Read(wkt);
        geometry.SRID = SystemReferenceId.SridLambert72;

        var result = ((LineString)geometry).TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.GeometryType.Should().Be("LineString");
    }

    [Fact]
    public void WhenTransformingMultiPoint_GivenLambert72MultiPoint_ThenTransformsToLambert08()
    {
        var wkt = "MULTIPOINT ((100000 190000), (100100 190100))";
        var geometry = new WKTReader(Lambert72Factory).Read(wkt);
        geometry.SRID = SystemReferenceId.SridLambert72;

        var result = ((MultiPoint)geometry).TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.GeometryType.Should().Be("MultiPoint");
    }

    [Fact]
    public void WhenTransformingMultiLineString_GivenLambert72MultiLineString_ThenTransformsToLambert08()
    {
        var wkt = "MULTILINESTRING ((100000 190000, 100100 190100), (100200 190200, 100300 190300))";
        var geometry = new WKTReader(Lambert72Factory).Read(wkt);
        geometry.SRID = SystemReferenceId.SridLambert72;

        var result = ((MultiLineString)geometry).TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.GeometryType.Should().Be("MultiLineString");
    }

    [Fact]
    public void WhenTransformingMultiPolygon_GivenLambert72MultiPolygon_ThenTransformsToLambert08()
    {
        var wkt = "MULTIPOLYGON (((100000 190000, 100100 190000, 100100 190100, 100000 190100, 100000 190000)), ((100200 190200, 100300 190200, 100300 190300, 100200 190300, 100200 190200)))";
        var geometry = new WKTReader(Lambert72Factory).Read(wkt);
        geometry.SRID = SystemReferenceId.SridLambert72;

        var result = ((MultiPolygon)geometry).TransformFromLambert72To08();

        result.SRID.Should().Be(SystemReferenceId.SridLambert2008);
        result.GeometryType.Should().Be("MultiPolygon");
    }

    #endregion
}
