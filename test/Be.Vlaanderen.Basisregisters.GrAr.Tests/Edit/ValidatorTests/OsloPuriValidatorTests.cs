namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Edit.ValidatorTests
{
    using FluentAssertions;
    using GrAr.Edit.Validators;
    using Xunit;

    public class OsloPuriValidatorTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("test/123", false)]
        [InlineData("http://test/123", true)]
        public void GivenPuri_ThenExpectedResult(string puri, bool expectedResult)
        {
            OsloPuriValidator.TryParseIdentifier(puri, out _).Should().Be(expectedResult);
        }
    }
}
