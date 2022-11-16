namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using NodaTime;
    using System;

    public static class NodaHelpers
    {
        public static readonly DateTimeZone? BelgianDateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Europe/Brussels");

        public static DateTimeOffset ToBelgianDateTimeOffset(this Instant value)
            => value.InZone(BelgianDateTimeZone ?? throw new InvalidOperationException($"BelgianDateTimeZone is null")).ToDateTimeOffset();
    }
}
