namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.NTS
{
    using System;
    using System.Buffers.Binary;
    using FluentAssertions;
    using GrAr.Common;
    using GrAr.Common.NetTopology;
    using Xunit;

    public sealed class WKBReaderFactoryTests
    {
        private const uint PointType = 1;
        private const uint EwkbSridFlag = ByteArrayExtensions.EwkbSridFlag;

        [Fact]
        public void WhenEwkbContainsLambert72Srid_ThenReturnsLambert72Reader()
        {
            var ewkb = BuildEwkbLittleEndian(SystemReferenceId.SridLambert72);

            var reader = WKBReaderFactory.CreateForEwkb(ewkb);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenEwkbContainsLambert2008Srid_ThenReturnsLambert2008Reader()
        {
            var ewkb = BuildEwkbLittleEndian(SystemReferenceId.SridLambert2008);

            var reader = WKBReaderFactory.CreateForEwkb(ewkb);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenEwkbIsBigEndianWithLambert72Srid_ThenReturnsReader()
        {
            var ewkb = BuildEwkbBigEndian(SystemReferenceId.SridLambert72);

            var reader = WKBReaderFactory.CreateForEwkb(ewkb);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenEwkbIsBigEndianWithLambert2008Srid_ThenReturnsReader()
        {
            var ewkb = BuildEwkbBigEndian(SystemReferenceId.SridLambert2008);

            var reader = WKBReaderFactory.CreateForEwkb(ewkb);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenEwkbIsNull_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkb(null!);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenEwkbIsTooShort_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkb(new byte[] { 1, 2, 3 });

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenEwkbIsEmpty_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkb(Array.Empty<byte>());

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenEwkbHasNoSridFlag_ThenThrowsArgumentException()
        {
            var wkb = BuildWkbWithoutSrid();

            var act = () => WKBReaderFactory.CreateForEwkb(wkb);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenEwkbContainsUnsupportedSrid_ThenThrowsInvalidOperationException()
        {
            var ewkb = BuildEwkbLittleEndian(srid: 4326); // WGS84 - not supported

            var act = () => WKBReaderFactory.CreateForEwkb(ewkb);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Unsupported SRID: 4326*");
        }

        [Fact]
        public void TryReadSrid_WhenLittleEndianLambert72_ThenReturnsCorrectSrid()
        {
            var ewkb = BuildEwkbLittleEndian(SystemReferenceId.SridLambert72);

            var result = ewkb.TryReadSrid(out var srid);

            result.Should().BeTrue();
            srid.Should().Be(SystemReferenceId.SridLambert72);
        }

        [Fact]
        public void TryReadSrid_WhenBigEndianLambert2008_ThenReturnsCorrectSrid()
        {
            var ewkb = BuildEwkbBigEndian(SystemReferenceId.SridLambert2008);

            var result = ewkb.TryReadSrid(out var srid);

            result.Should().BeTrue();
            srid.Should().Be(SystemReferenceId.SridLambert2008);
        }

        [Fact]
        public void TryReadSrid_WhenNull_ThenReturnsFalse()
        {
            byte[]? ewkb = null;

            var result = ewkb.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        [Fact]
        public void TryReadSrid_WhenTooShort_ThenReturnsFalse()
        {
            var ewkb = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }; // 8 bytes < 9

            var result = ewkb.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        [Fact]
        public void TryReadSrid_WhenExactlyNineBytes_AndHasSridFlag_ThenReturnsTrue()
        {
            // Minimal valid EWKB: 9 bytes with SRID flag
            var bytes = new byte[9];
            bytes[0] = 1; // little-endian
            BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(1, 4), PointType | EwkbSridFlag);
            BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(5, 4), SystemReferenceId.SridLambert72);

            var result = bytes.TryReadSrid(out var srid);

            result.Should().BeTrue();
            srid.Should().Be(SystemReferenceId.SridLambert72);
        }

        [Fact]
        public void TryReadSrid_WhenNoSridFlag_ThenReturnsFalse()
        {
            var wkb = BuildWkbWithoutSrid();

            var result = wkb.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        #region CreateForEwkbAsHex

        [Fact]
        public void WhenHexEwkbContainsLambert72Srid_ThenReturnsLambert72Reader()
        {
            var hex = ToHex(BuildEwkbLittleEndian(SystemReferenceId.SridLambert72));

            var reader = WKBReaderFactory.CreateForEwkbAsHex(hex);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenHexEwkbContainsLambert2008Srid_ThenReturnsLambert2008Reader()
        {
            var hex = ToHex(BuildEwkbLittleEndian(SystemReferenceId.SridLambert2008));

            var reader = WKBReaderFactory.CreateForEwkbAsHex(hex);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenHexEwkbIsBigEndianWithLambert72Srid_ThenReturnsReader()
        {
            var hex = ToHex(BuildEwkbBigEndian(SystemReferenceId.SridLambert72));

            var reader = WKBReaderFactory.CreateForEwkbAsHex(hex);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenHexEwkbIsBigEndianWithLambert2008Srid_ThenReturnsReader()
        {
            var hex = ToHex(BuildEwkbBigEndian(SystemReferenceId.SridLambert2008));

            var reader = WKBReaderFactory.CreateForEwkbAsHex(hex);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenHexEwkbContainsUpperCaseHex_ThenReturnsReader()
        {
            var hex = ToHex(BuildEwkbLittleEndian(SystemReferenceId.SridLambert72)).ToUpperInvariant();

            var reader = WKBReaderFactory.CreateForEwkbAsHex(hex);

            reader.Should().NotBeNull();
        }

        [Fact]
        public void WhenHexEwkbIsNull_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkbAsHex(null!);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbIsEmpty_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkbAsHex(string.Empty);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbIsTooShort_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkbAsHex("0102030405");

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbIsNotValidHex_ThenThrowsArgumentException()
        {
            var act = () => WKBReaderFactory.CreateForEwkbAsHex("ZZZZZZZZZZZZZZZZZZ");

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbHasOddLength_ThenThrowsArgumentException()
        {
            // Odd-length string is not a valid hex byte array
            var act = () => WKBReaderFactory.CreateForEwkbAsHex("012");

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbHasNoSridFlag_ThenThrowsArgumentException()
        {
            var hex = ToHex(BuildWkbWithoutSrid());

            var act = () => WKBReaderFactory.CreateForEwkbAsHex(hex);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No SrID found*");
        }

        [Fact]
        public void WhenHexEwkbContainsUnsupportedSrid_ThenThrowsInvalidOperationException()
        {
            var hex = ToHex(BuildEwkbLittleEndian(srid: 4326)); // WGS84 - not supported

            var act = () => WKBReaderFactory.CreateForEwkbAsHex(hex);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Unsupported SRID: 4326*");
        }

        [Fact]
        public void TryReadSridHex_WhenLittleEndianLambert72_ThenReturnsCorrectSrid()
        {
            var hex = ToHex(BuildEwkbLittleEndian(SystemReferenceId.SridLambert72));

            var result = hex.TryReadSrid(out var srid);

            result.Should().BeTrue();
            srid.Should().Be(SystemReferenceId.SridLambert72);
        }

        [Fact]
        public void TryReadSridHex_WhenBigEndianLambert2008_ThenReturnsCorrectSrid()
        {
            var hex = ToHex(BuildEwkbBigEndian(SystemReferenceId.SridLambert2008));

            var result = hex.TryReadSrid(out var srid);

            result.Should().BeTrue();
            srid.Should().Be(SystemReferenceId.SridLambert2008);
        }

        [Fact]
        public void TryReadSridHex_WhenNull_ThenReturnsFalse()
        {
            string? hex = null;

            var result = hex!.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        [Fact]
        public void TryReadSridHex_WhenEmpty_ThenReturnsFalse()
        {
            var result = string.Empty.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        [Fact]
        public void TryReadSridHex_WhenNotValidHex_ThenReturnsFalse()
        {
            var result = "not-hex-at-all!".TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        [Fact]
        public void TryReadSridHex_WhenNoSridFlag_ThenReturnsFalse()
        {
            var hex = ToHex(BuildWkbWithoutSrid());

            var result = hex.TryReadSrid(out var srid);

            result.Should().BeFalse();
            srid.Should().Be(0);
        }

        #endregion

        /// <summary>
        /// Builds a minimal EWKB byte array (little-endian) with the given SRID embedded.
        /// Layout: [endian(1)] [type+srid_flag(4)] [srid(4)] [x(8)] [y(8)] = 25 bytes
        /// </summary>
        private static byte[] BuildEwkbLittleEndian(int srid, double x = 0, double y = 0)
        {
            var bytes = new byte[25];
            bytes[0] = 1; // little-endian
            BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(1, 4), PointType | EwkbSridFlag);
            BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(5, 4), srid);
            BinaryPrimitives.WriteDoubleLittleEndian(bytes.AsSpan(9, 8), x);
            BinaryPrimitives.WriteDoubleLittleEndian(bytes.AsSpan(17, 8), y);
            return bytes;
        }

        /// <summary>
        /// Builds a minimal EWKB byte array (big-endian) with the given SRID embedded.
        /// </summary>
        private static byte[] BuildEwkbBigEndian(int srid, double x = 0, double y = 0)
        {
            var bytes = new byte[25];
            bytes[0] = 0; // big-endian
            BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(1, 4), PointType | EwkbSridFlag);
            BinaryPrimitives.WriteInt32BigEndian(bytes.AsSpan(5, 4), srid);
            BinaryPrimitives.WriteDoubleBigEndian(bytes.AsSpan(9, 8), x);
            BinaryPrimitives.WriteDoubleBigEndian(bytes.AsSpan(17, 8), y);
            return bytes;
        }

        /// <summary>
        /// Builds a WKB byte array (little-endian) without the SRID flag set.
        /// Layout: [endian(1)] [type(4)] [x(8)] [y(8)] = 21 bytes
        /// </summary>
        private static byte[] BuildWkbWithoutSrid()
        {
            var bytes = new byte[21];
            bytes[0] = 1; // little-endian
            BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(1, 4), PointType); // no SRID flag
            BinaryPrimitives.WriteDoubleLittleEndian(bytes.AsSpan(5, 8), 0);
            BinaryPrimitives.WriteDoubleLittleEndian(bytes.AsSpan(13, 8), 0);
            return bytes;
        }

        /// <summary>
        /// Converts a byte array to its lowercase hex string representation.
        /// E.g. { 0x01, 0xFF } → "01ff"
        /// </summary>
        private static string ToHex(byte[] bytes) => Utilities.HexByteConvertor.ByteArrayExtensions.ToHexString(bytes)!;
    }
}

