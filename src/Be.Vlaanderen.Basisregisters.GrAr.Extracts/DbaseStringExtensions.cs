namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System;
    using Shaperon;

    public static class DbaseStringExtensions
    {
        public static string FromDateTimeOffset(this DateTimeOffset dateTimeOffset)
            => dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss%K");

        public static void SetValue(this DbaseCharacter dbaseString, DateTimeOffset dateTimeOffset)
            => dbaseString.Value = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss%K");
    }
}
