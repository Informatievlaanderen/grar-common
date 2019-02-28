namespace Be.Vlaanderen.Basisregisters.GrAr.Tests
{
    using Common;
    using FluentAssertions;
    using NodaTime.Extensions;
    using System;
    using Xunit;

    public class NodaHelperTests
    {
        [Fact]
        public void WhenConvertingBelgianDateTime_GivenDateTime_ThenOffset_is_set()
        {
            var dateTime = new DateTime(2019, 1, 1, 23, 0, 0, DateTimeKind.Utc);
            var utcDateTime = new DateTimeOffset(dateTime);
            var instantDate = utcDateTime.ToInstant();

            var expectedOffset = 1;
            instantDate.ToBelgianDateTimeOffset().Should().Be(new DateTimeOffset(2019, 1, 2, 0, 0, 0, TimeSpan.FromHours(expectedOffset)));
        }
    }
}
