namespace Be.Vlaanderen.Basisregisters.GrAr.Common.NetTopology;

using System;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;
using NetTopologySuite.IO;

public static class WKBReaderFactory
{
    public static WKBReader CreateForLambert72() => new WKBReader(NtsGeometryFactory.CreateNtsGeometryServicesLambert72());
    public static WKBReader CreateForLambert2008() => new WKBReader(NtsGeometryFactory.CreateNtsGeometryServicesLambert2008());

    public static WKBReader CreateForEwkb(byte[] ewkb)
    {
        if (!ewkb.TryReadSrid(out var srid))
            throw new ArgumentException("No SrID found in EWKB.", nameof(ewkb));

        return srid switch
        {
            SystemReferenceId.SridLambert72 => CreateForLambert72(),
            SystemReferenceId.SridLambert2008 => CreateForLambert2008(),
            _ => throw new InvalidOperationException($"Unsupported SRID: {srid}.")
        };
    }

    public static WKBReader CreateForEwkbAsHex(string ewkbAsHex)
    {
        if (!ewkbAsHex.TryReadSrid(out var srid))
            throw new ArgumentException("No SrID found in EWKB.", nameof(ewkbAsHex));

        return srid switch
        {
            SystemReferenceId.SridLambert72 => CreateForLambert72(),
            SystemReferenceId.SridLambert2008 => CreateForLambert2008(),
            _ => throw new InvalidOperationException($"Unsupported SRID: {srid}.")
        };
    }
}

public static class NtsGeometryFactory
{
    public static NtsGeometryServices CreateNtsGeometryServicesLambert72() =>
        new NtsGeometryServices(
            new DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY),
            new PrecisionModel(PrecisionModels.Floating),
            SystemReferenceId.SridLambert72);

    public static NtsGeometryServices CreateNtsGeometryServicesLambert2008() =>
        new NtsGeometryServices(
            new DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY),
            new PrecisionModel(PrecisionModels.Floating),
            SystemReferenceId.SridLambert2008);

    public static NetTopologySuite.Geometries.GeometryFactory CreateGeometryFactoryLambert72() => CreateNtsGeometryServicesLambert72().CreateGeometryFactory();
    public static NetTopologySuite.Geometries.GeometryFactory CreateGeometryFactoryLambert2008() => CreateNtsGeometryServicesLambert2008().CreateGeometryFactory();
}
