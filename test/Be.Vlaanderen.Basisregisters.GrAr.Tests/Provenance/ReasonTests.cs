namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance
{
    using System;
    using Crab;
    using FluentAssertions;
    using GrAr.Provenance;
    using NodaTime;
    using Xunit;

    public class ReasonTests
    {
        [Fact]
        public void GivenNullOperatorThenReasonShouldBeCrabManagement()
        {
            var provenance = new CrabProvenanceFactory()
                    .CreateFrom(
                        0,
                        false,
                        new CrabTimestamp(Instant.FromDateTimeOffset(DateTimeOffset.Now)),
                        CrabModification.Insert,
                        null,
                        CrabOrganisation.DePost);

            provenance.Reason.Should().Be(Reason.ManagementCrab);
        }
    }
}
