namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using NodaTime;
    using System;
    using System.Globalization;

    public static class NodaHelpers
    {
        public static DateTimeZone BelgianDateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Brussels");

        public static DateTimeOffset ToBelgianDateTimeOffset(this Instant value)
            => value.InZone(BelgianDateTimeZone).ToDateTimeOffset();

        public static string ToIso8601(this Instant value)
            => value.ToString("uuuu'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFFF'Z'", CultureInfo.InvariantCulture);
    }
}
