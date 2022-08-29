namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Extracts
{
    using System;
    using Be.Vlaanderen.Basisregisters.GrAr.Extracts;
    using Shaperon;
    using FluentAssertions;
    using Xunit;

    public class DbaseStringExtensionsTests
    {
        [Fact]
        public void WhenFromDateTime_GivenDateTimeOffset_ThenShouldBeExpectedString()
        {
            var expected = "2019-11-28T14:55:30+01:00";
            var dateTimeOffset = new DateTimeOffset(2019, 11, 28, 14, 55, 30, TimeSpan.FromHours(1));

            dateTimeOffset.FromDateTimeOffset().Should().Be(expected);
        }

        [Fact]
        public void WhenSettingValue_GivenDateTimeOffset_ThenShouldBeExpectedDbaseString()
        {
            var expected = "2019-11-28T14:55:30+01:00";
            var dateTimeOffset = new DateTimeOffset(2019, 11, 28, 14, 55, 30, TimeSpan.FromHours(1));

            var dbaseString = new DbaseCharacter(DbaseField.CreateCharacterField(new DbaseFieldName("test"), new DbaseFieldLength(25)));
            dbaseString.SetValue(dateTimeOffset);

            dbaseString.Value.Should().Be(expected);
        }
    }
}
