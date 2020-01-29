namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryTypes
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using Infrastructure;
    using Legacy.SpatialTools.GeometryTypes;
    using Xunit;

    public class WhenConvertingACreatingAGeometryCoordinateValueBackToADouble
    {
        private readonly double _originalValue;
        private readonly GeometryCoordinateValue _coordinateValue;

        public WhenConvertingACreatingAGeometryCoordinateValueBackToADouble()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 25);

            _coordinateValue = new GeometryCoordinateValue(_originalValue);
        }

        [Fact]
        public void ThenOriginalPrecisionShouldBeMaintained()
        {
            _coordinateValue
                .Should()
                .Be(_originalValue);
        }

    }

    public class WhenWritingAGeometryCoordinateValueWithLessPrecisionThanExpected
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAGeometryCoordinateValueWithLessPrecisionThanExpected()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(0, 10);

            _printedValue = new GeometryCoordinateValue(_originalValue).ToString();
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
            double.Parse(_printedValue)
                .Should()
                .Be(_originalValue);
        }
    }

    public class WhenWritingAGeometryCoordinateValueWithMorePrecisionThanSupported
    {
        private readonly double _originalValue;
        private readonly string _printedValue;

        public WhenWritingAGeometryCoordinateValueWithMorePrecisionThanSupported()
        {
            _originalValue = new Fixture().CreateDoubleWithPrecision(12, 20);

            _printedValue = new GeometryCoordinateValue(_originalValue).ToString();
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
            double.Parse(_printedValue)
                .Should()
                .Be(roundedValue);
        }
    }

    public class WhenParsingAValidDoubleStringAsGeometryCoordinateValue
    {
        [Fact]
        public void ThenAGeometryCoordinateValueShouldBeReturned()
        {
            var value = new Fixture().CreateDoubleWithPrecision(15);
            GeometryCoordinateValue
                .TryParse($"{value}")
                .Should()
                .NotBeNull()
                .And.BeOfType<GeometryCoordinateValue>()
                .And.Be(value);
        }
    }

    public class WhenParsingAnInvalidDoubleStringAsGeometryCoordinateValue
    {
        [Fact]
        public void ThenNothingShouldBeReturned()
        {
            GeometryCoordinateValue
                .TryParse(new Fixture().Create<string>())
                .Should()
                .BeNull();
        }
    }
}
