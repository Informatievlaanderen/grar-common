namespace Be.Vlaanderen.Basisregisters.GrAr.Common;

using System;
using System.Buffers.Binary;

public static class ByteArrayExtensions
{
    public const uint EwkbSridFlag = 0x20000000;

    public static bool TryReadSrid(this byte[]? ewkb, out int srid)
    {
        srid = 0;
        if (ewkb == null || ewkb.Length < 9)
            return false;

        var littleEndian = ewkb[0] == 1;
        var type = littleEndian
            ? BinaryPrimitives.ReadUInt32LittleEndian(ewkb.AsSpan(1, 4))
            : BinaryPrimitives.ReadUInt32BigEndian(ewkb.AsSpan(1, 4));

        if ((type & EwkbSridFlag) == 0)
            return false;

        srid = littleEndian
            ? BinaryPrimitives.ReadInt32LittleEndian(ewkb.AsSpan(5, 4))
            : BinaryPrimitives.ReadInt32BigEndian(ewkb.AsSpan(5, 4));

        return true;
    }
}
