namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates
{
    using System;
    using System.Globalization;
    using AutoFixture;
    using FluentAssertions;
    using GrAr.Common.SpatialTools.GeometryCoordinates;
    using Infrastructure;
    using Xunit;

    public class WhenConvertingACreatingALineStringGeometryCoordinateValueBackToADouble
    {
        private readonly double _originalValue;
        private readonly GeometryCoordinateValue _coordinateValue;

        public WhenConvertingACreatingALineStringGeometryCoordinateValueBackToADouble()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 25);

            _coordinateValue = new LineStringGeometryCoordinateValue(_originalValue);
        }

        [Fact]
        public void ThenOriginalPrecisionShouldBeMaintained()
        {
            _coordinateValue
                .Should()
                .Be(_originalValue);
        }

    }

    public class WhenWritingALineStringGeometryCoordinateValueWithLessPrecisionThanExpected
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingALineStringGeometryCoordinateValueWithLessPrecisionThanExpected()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(0, 10);

            _printedValue = new LineStringGeometryCoordinateValue(_originalValue).ToString();
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

    public class WhenWritingALineStringGeometryCoordinateValueWithMorePrecisionThanSupported
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingALineStringGeometryCoordinateValueWithMorePrecisionThanSupported()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 20);

            _printedValue = new LineStringGeometryCoordinateValue(_originalValue).ToString();
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

    public class WhenParsingAValidDoubleStringAsLineStringGeometryCoordinateValue
    {
        [Fact]
        public void ThenAGeometryCoordinateValueShouldBeReturned()
        {
            var value = new Fixture().CreateDoubleWithPrecision(15);
            LineStringGeometryCoordinateValue
                .TryParse(value.ToString(CultureInfo.InvariantCulture))
                .Should()
                .NotBeNull()
                .And.BeOfType<LineStringGeometryCoordinateValue>()
                .And.Be(value);
        }
    }

    public class WhenParsingAnInvalidDoubleStringAsLineStringGeometryCoordinateValue
    {
        [Fact]
        public void ThenNothingShouldBeReturned()
        {
            LineStringGeometryCoordinateValue
                .TryParse(new Fixture().Create<string>())
                .Should()
                .BeNull();
        }
    }
}
