namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates
{
    using System;
    using System.Globalization;
    using AutoFixture;
    using FluentAssertions;
    using Infrastructure;
    using Legacy.SpatialTools;
    using Xunit;

    public class WhenConvertingACreatingAPointGeometryCoordinateValueBackToADouble
    {
        private readonly double _originalValue;
        private readonly GeometryCoordinateValue _coordinateValue;

        public WhenConvertingACreatingAPointGeometryCoordinateValueBackToADouble()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(3, 12);

            _coordinateValue = new PointGeometryCoordinateValue(_originalValue);
        }

        [Fact]
        public void ThenOriginalPrecisionShouldBeMaintained()
        {
            _coordinateValue
                .Should()
                .Be(_originalValue);
        }

    }

    public class WhenWritingAPointGeometryCoordinateValueWithLessPrecisionThanExpected
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAPointGeometryCoordinateValueWithLessPrecisionThanExpected()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(0, 1);

            _printedValue = new PointGeometryCoordinateValue(_originalValue).ToString();
        }

        [Fact]
        public void ThenTheDecimalPrecisionShouldBeExtendedToShowElevenDecimals()
        {
            _printedValue
                .Should()
                .MatchRegex(@"^-?\d+\.\d{2}$");
        }

        [Fact]
        public void ThenPrintedValueShouldBeEqualToTheOriginal()
        {
            double.Parse(_printedValue, CultureInfo.InvariantCulture)
                .Should()
                .Be(_originalValue);
        }
    }

    public class WhenWritingAPointGeometryCoordinateValueWithMorePrecisionThanSupported
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAPointGeometryCoordinateValueWithMorePrecisionThanSupported()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(3, 15);

            _printedValue = new PointGeometryCoordinateValue(_originalValue).ToString();
        }

        [Fact]
        public void ThenTheDecimalPrecisionShouldBeReducedToElevenDecimals()
        {
            _printedValue
                .Should()
                .MatchRegex(@"^-?\d+\.\d{2}$");
        }

        [Fact]
        public void ThenThePrintedValueShouldBeRoundedToElevenDecimalsPrecision()
        {
            var roundedValue = Math.Round(_originalValue, 2);
            double.Parse(_printedValue, CultureInfo.InvariantCulture)
                .Should()
                .Be(roundedValue);
        }
    }

    public class WhenParsingAValidDoubleStringAsPointGeometryCoordinateValue
    {
        [Fact]
        public void ThenAGeometryCoordinateValueShouldBeReturned()
        {
            var value = new Fixture().CreateDoubleWithPrecision(15);
            PointGeometryCoordinateValue
                .TryParse(value.ToString(CultureInfo.InvariantCulture))
                .Should()
                .NotBeNull()
                .And.BeOfType<PointGeometryCoordinateValue>()
                .And.Be(value);
        }
    }

    public class WhenParsingAnInvalidDoubleStringAsPointGeometryCoordinateValue
    {
        [Fact]
        public void ThenNothingShouldBeReturned()
        {
            PointGeometryCoordinateValue
                .TryParse(new Fixture().Create<string>())
                .Should()
                .BeNull();
        }
    }
}
