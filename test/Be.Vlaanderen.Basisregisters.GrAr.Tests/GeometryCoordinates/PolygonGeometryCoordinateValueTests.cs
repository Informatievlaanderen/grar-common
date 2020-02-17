namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates
{
    using System;
    using System.Globalization;
    using AutoFixture;
    using FluentAssertions;
    using Infrastructure;
    using Legacy.SpatialTools;
    using Xunit;

    public class WhenConvertingACreatingAPolygonGeometryCoordinateValueBackToADouble
    {
        private readonly double _originalValue;
        private readonly GeometryCoordinateValue _coordinateValue;

        public WhenConvertingACreatingAPolygonGeometryCoordinateValueBackToADouble()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 25);

            _coordinateValue = new PolygonGeometryCoordinateValue(_originalValue);
        }

        [Fact]
        public void ThenOriginalPrecisionShouldBeMaintained()
        {
            _coordinateValue
                .Should()
                .Be(_originalValue);
        }

    }

    public class WhenWritingAPolygonGeometryCoordinateValueWithLessPrecisionThanExpected
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAPolygonGeometryCoordinateValueWithLessPrecisionThanExpected()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(0, 10);

            _printedValue = new PolygonGeometryCoordinateValue(_originalValue).ToString();
        }

        [Fact]
        public void ThenTheDecimalPrecisionShouldBeExtendedToShowElevenDecimals()
        {
            _printedValue
                .Should()
                .MatchRegex(@"^-?\d+\.\d{11}$");
        }

        [Fact]
        public void ThenPrintedValueShouldBeEqualToTheOriginal()
        {
            double.Parse(_printedValue, CultureInfo.InvariantCulture)
                .Should()
                .Be(_originalValue);
        }
    }

    public class WhenWritingAPolygonGeometryCoordinateValueWithMorePrecisionThanSupported
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAPolygonGeometryCoordinateValueWithMorePrecisionThanSupported()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 20);

            _printedValue = new PolygonGeometryCoordinateValue(_originalValue).ToString();
        }

        [Fact]
        public void ThenTheDecimalPrecisionShouldBeReducedToElevenDecimals()
        {
            _printedValue
                .Should()
                .MatchRegex(@"^-?\d+\.\d{11}$");
        }

        [Fact]
        public void ThenThePrintedValueShouldBeRoundedToElevenDecimalsPrecision()
        {
            var roundedValue = Math.Round(_originalValue, 11);
            double.Parse(_printedValue, CultureInfo.InvariantCulture)
                .Should()
                .Be(roundedValue);
        }
    }

    public class WhenParsingAValidDoubleStringAsPolygonGeometryCoordinateValue
    {
        [Fact]
        public void ThenAGeometryCoordinateValueShouldBeReturned()
        {
            var value = new Fixture().CreateDoubleWithPrecision(15);
            PolygonGeometryCoordinateValue
                .TryParse(value.ToString(CultureInfo.InvariantCulture))
                .Should()
                .NotBeNull()
                .And.BeOfType<PolygonGeometryCoordinateValue>()
                .And.Be(value);
        }
    }

    public class WhenParsingAnInvalidDoubleStringAsPolygonGeometryCoordinateValue
    {
        [Fact]
        public void ThenNothingShouldBeReturned()
        {
            PolygonGeometryCoordinateValue
                .TryParse(new Fixture().Create<string>())
                .Should()
                .BeNull();
        }
    }
}
