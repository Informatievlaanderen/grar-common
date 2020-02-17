namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.GeometryCoordinates.Infrastructure
{
    using System;
    using System.Globalization;
    using AutoFixture;

    public static class FixtureExtensions {

        public static double CreateDoubleWithPrecision(this IFixture fixture, int precision)
        {
            if (precision < 0 )
                throw new InvalidDecimalPrecisionException(nameof(precision));

            var random = new Random(fixture.Create<int>());

            var integer = fixture.Create<int>();
            var fraction = random
                .Next(1001, int.MaxValue)
                .ToString()
                .TrimEnd('0')
                .PadLeft(precision, '0')
                .Substring(0, precision);

            return double.Parse($"{integer}.{fraction}", CultureInfo.InvariantCulture);
        }

        public static double CreateDoubleWithPrecision(this IFixture fixture, int minimumPrecision, int maximumPrecision)
            => minimumPrecision < 0
                ? throw new InvalidDecimalPrecisionException(nameof(minimumPrecision))
                : fixture.CreateDoubleWithPrecision(new Random().Next(minimumPrecision, maximumPrecision + 1));
    }

    internal class InvalidDecimalPrecisionException : ArgumentException
    {
        public InvalidDecimalPrecisionException(string parameterName)
            : base($"{parameterName} can't be less than 0")
        { }
    }
}
