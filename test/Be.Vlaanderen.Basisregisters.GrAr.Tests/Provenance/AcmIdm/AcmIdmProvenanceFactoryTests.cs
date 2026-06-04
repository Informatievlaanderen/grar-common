namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Provenance.AcmIdm
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using AutoFixture;
    using Basisregisters.Auth.AcmIdm;
    using FluentAssertions;
    using GrAr.Provenance;
    using GrAr.Provenance.AcmIdm;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Moq;
    using NodaTime;
    using Xunit;

    public class AcmIdmProvenanceFactoryTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IHttpContextAccessor> _mockHttpContext;

        public AcmIdmProvenanceFactoryTests()
        {
            _mockHttpContext = new Mock<IHttpContextAccessor>();
            _fixture = new Fixture();
            var defaultClaims = new List<Claim>
            {
                new Claim(AcmIdmClaimTypes.VoOrgCode, _fixture.Create<string>()),
            };

            _mockHttpContext.SetupProperty(x => x.HttpContext, new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims, "Test"))
            });
        }

        [Fact]
        public void TimestampCloseToNowTest()
        {
            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result
                .Timestamp
                .ToDateTimeOffset()
                .Should()
                .BeCloseTo(SystemClock.Instance.GetCurrentInstant().ToDateTimeOffset(), TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData(Application.Grb)]
        [InlineData(Application.BuildingRegistry)]
        [InlineData(Application.AddressRegistry)]
        [InlineData(Application.ParcelRegistry)]
        [InlineData(Application.PostalRegistry)]
        [InlineData(Application.MunicipalityRegistry)]
        [InlineData(Application.StreetNameRegistry)]
        [InlineData(Application.Lara)]
        [InlineData(Application.BPost)]
        [InlineData(Application.RoadRegistry)]
        [InlineData(Application.GrbCrabMatching)]
        [InlineData(Application.CrabSsisService)]
        [InlineData(Application.GrbCrabMatching)]
        [InlineData(Application.CrabWstEditService)]
        public void ApplicationTest(Application application)
        {
            var factory = new AcmIdmProvenanceFactory(application, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Application.Should().Be(application);
        }

        [Fact]
        public void ReasonTest()
        {
            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var reason = _fixture.Create<string>();
            var result = factory.Create(new Reason(reason), _fixture.Create<Modification>());

            result.Reason.ToString().Should().Be(reason);
        }

        [Theory]
        [InlineData(Modification.Delete)]
        [InlineData(Modification.Insert)]
        [InlineData(Modification.Update)]
        [InlineData(Modification.Unknown)]
        public void ModificationTest(Modification modification)
        {
            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), modification);

            result.Modification.Should().Be(modification);
        }

        [Fact]
        public void OperatorTest()
        {
            var expectedOrgCode = _fixture.Create<string>();
            var defaultClaims = new List<Claim>
            {
                new Claim(AcmIdmClaimTypes.VoOrgCode, expectedOrgCode),
            };

            _mockHttpContext.SetupProperty(x => x.HttpContext, new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims, "Test"))
            });

            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Operator.ToString().Should().Be(expectedOrgCode);
        }

        [Fact]
        public void OperatorWithOvoCodeTest()
        {
            var expectedOvoCode = "OVO" + _fixture.Create<string>();
            var defaultClaims = new List<Claim>
            {
                new Claim(AcmIdmClaimTypes.VoOrgCode, _fixture.Create<string>()),
                new Claim(AcmIdmClaimTypes.VoOvoCode, expectedOvoCode),
            };

            _mockHttpContext.SetupProperty(x => x.HttpContext, new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims, "Test"))
            });

            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Operator.ToString().Should().Be(expectedOvoCode);
        }

        [Fact]
        public void OrganisationDigitaalVlaanderenTest()
        {
            var defaultClaims = new List<Claim>
            {
                new Claim(AcmIdmClaimTypes.VoOrgCode, AcmIdmProvenanceFactory.OvoCodeDigitaalVlaanderen),
            };

            _mockHttpContext.SetupProperty(x => x.HttpContext, new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims, "Test"))
            });

            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Organisation.Should().Be(Organisation.DigitaalVlaanderen);
        }

        [Fact]
        public void OrganisationMunicipalityTest()
        {
            var defaultClaims = new List<Claim>
            {
                new Claim(AcmIdmClaimTypes.VoOrgCode, _fixture.Create<string>()),
                new Claim(AcmIdmClaimTypes.NisCode, _fixture.Create<string>()),
            };

            _mockHttpContext.SetupProperty(x => x.HttpContext, new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims, "Test"))
            });

            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Organisation.Should().Be(Organisation.Municipality);
        }

        [Fact]
        public void OrganisationOtherTest()
        {
            var factory = new AcmIdmProvenanceFactory(Application.BuildingRegistry, _mockHttpContext.Object);
            var result = factory.Create(_fixture.Create<Reason>(), _fixture.Create<Modification>());

            result.Organisation.Should().Be(Organisation.Other);
        }
    }
}
