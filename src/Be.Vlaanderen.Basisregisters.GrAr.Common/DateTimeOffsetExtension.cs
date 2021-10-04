namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System;

    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset ToExampleOffset(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToOffset(new TimeSpan(2, 0, 0));
        }
    }
}
