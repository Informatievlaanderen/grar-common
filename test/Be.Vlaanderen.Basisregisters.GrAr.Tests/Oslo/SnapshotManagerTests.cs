namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Oslo
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using GrAr.Oslo.SnapshotProducer;
    using Microsoft.Extensions.Logging.Abstractions;
    using Moq;
    using NodaTime;
    using Xunit;

    public sealed class SnapshotManagerTests
    {
        [Fact(Skip = "Tool to test SnapshotManager.")]
        public async Task T()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.basisregisters.staging-vlaanderen.be/v2/straatnamen");
            var proxy = new OsloProxy(httpClient);
            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), proxy,SnapshotManagerOptions.Create("1", "1"));
            var result = await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse("2022-03-23T14:24:04+01:00")),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            result.Should().NotBeNull();
        }

        [Fact]
        public void WhenVersionsMatch_ThenReturnOsloResult()
        {
            var eventVersion = "2022-03-23T14:24:04.801+01:00";
            var snapshotVersion = "2022-03-23T14:24:04+01:00";

            var ct = new CancellationTokenSource(5000);

            var mockProxy = new Mock<IOsloProxy>();
            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new OsloResult
                {
                    Identificator = new OsloIdentificator
                    {
                        Versie = snapshotVersion
                    }
                });

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, SnapshotManagerOptions.Create("1", "1"));
            var result = snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse(eventVersion)),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            Task.WaitAny(new Task[] { result }, ct.Token);

            result.Result.Should().NotBeNull();
        }

        [Fact]
        public void WhenEventVersionOlderThanSnapshot_ThenReturnNull()
        {
            var eventVersion = $"202{1}-03-23T14:24:04.801+01:00";
            var snapshotVersion = "2022-03-23T14:24:04+01:00";

            var ct = new CancellationTokenSource(5000);

            var mockProxy = new Mock<IOsloProxy>();
            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new OsloResult
                {
                    Identificator = new OsloIdentificator
                    {
                        Versie = snapshotVersion
                    }
                });

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, SnapshotManagerOptions.Create("1", "1"));
            var result = snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse(eventVersion)),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            Task.WaitAny(new Task[] { result }, ct.Token);

            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task WhenStaleSnapshot_ThenRetry()
        {
            var options = SnapshotManagerOptions.Create("2", "1");

            var eventVersion = $"2022-03-23T1{4}:24:04+01:00";
            var staleSnapshotVersion = $"2022-03-23T1{1}:24:04+01:00";

            var count = 0;

            var mockProxy = new Mock<IOsloProxy>();

            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(() =>
                {
                    if (count == options.MaxRetryWaitIntervalSeconds)
                    {
                        // Return snapshot with correct version
                        return new OsloResult
                        {
                            Identificator = new OsloIdentificator
                            {
                                Versie = eventVersion
                            }
                        };
                    }

                    count++;

                    // Return stale snapshot
                    return new OsloResult
                    {
                        Identificator = new OsloIdentificator
                        {
                            Versie = staleSnapshotVersion
                        }
                    };
                });

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, options);
            var result = await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse(eventVersion)),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            result.Should().NotBeNull();
            mockProxy.Verify(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None), () => Times.AtMost(options.MaxRetryWaitIntervalSeconds + 1));
        }

        [Fact]
        public async Task WhenGetSnapshotResponse410AndThrowStaleWhenGone_ThenRetry()
        {
            var options = SnapshotManagerOptions.Create("2", "1");

            var throwStaleWhenGone = true;

            var eventVersion = "2022-03-23T14:24:04+01:00";

            var count = 0;

            var mockProxy = new Mock<IOsloProxy>();

            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(() =>
                {
                    // Circuit breaker
                    if (count == options.MaxRetryWaitIntervalSeconds)
                    {
                        return new OsloResult
                        {
                            Identificator = new OsloIdentificator
                            {
                                Versie = eventVersion
                            }
                        };
                    }

                    count++;

                    throw new HttpRequestException(string.Empty, null, HttpStatusCode.Gone);
                });

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, options);
            var result = await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse(eventVersion)),
                eventPosition: 1111111,
                throwStaleWhenGone,
                CancellationToken.None);

            result.Should().NotBeNull();
            mockProxy.Verify(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None), () => Times.AtMost(options.MaxRetryWaitIntervalSeconds + 1));
        }

        [Fact]
        public async Task WhenGetSnapshotResponse410_ThenReturnNull()
        {
            var options = SnapshotManagerOptions.Create("2", "1");

            var doNotThrowStaleWhenGone = false;

            var mockProxy = new Mock<IOsloProxy>();

            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Gone));

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, options);
            var result = await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse("2022-03-23T14:24:04+01:00")),
                eventPosition: 1111111,
                doNotThrowStaleWhenGone,
                CancellationToken.None);

            result.Should().BeNull();
            mockProxy.Verify(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None), Times.AtMostOnce);
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.MethodNotAllowed)]
        [InlineData(HttpStatusCode.NotAcceptable)]
        [InlineData(HttpStatusCode.RequestTimeout)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.PreconditionFailed)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.NotImplemented)]
        [InlineData(HttpStatusCode.BadGateway)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public async Task WhenGetSnapshotResponseInList_ThenRetry(HttpStatusCode httpStatusCode)
        {
            var options = SnapshotManagerOptions.Create("1", "0");

            var eventVersion = "2022-03-23T14:24:04+01:00";

            var count = 0;

            var mockProxy = new Mock<IOsloProxy>();

            mockProxy.Setup(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(() =>
                {
                    if (count == options.MaxRetryWaitIntervalSeconds)
                    {
                        // Circuit breaker
                        return new OsloResult
                        {
                            Identificator = new OsloIdentificator
                            {
                                Versie = eventVersion
                            }
                        };
                    }

                    count++;

                    throw new HttpRequestException(string.Empty, null, httpStatusCode);
                });

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, options);
            await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse(eventVersion)),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            mockProxy.Verify(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None), () => Times.Exactly(options.MaxRetryWaitIntervalSeconds + 1));
        }

        [Fact]
        public async Task WhenUnhandledException_ThenRethrow()
        {
            var mockProxy = new Mock<IOsloProxy>();

            mockProxy
                .Setup(x => x.GetSnapshot(It.IsAny<string>(), CancellationToken.None))
                .Throws<ArgumentException>();

            var snapshotManager = new SnapshotManager(new NullLoggerFactory(), mockProxy.Object, SnapshotManagerOptions.Create("1", "0"));
            var act = async () => await snapshotManager.FindMatchingSnapshot(
                "50083",
                Instant.FromDateTimeOffset(DateTimeOffset.Parse("2022-03-23T14:24:04+01:00")),
                eventPosition: 1111111,
                throwStaleWhenGone: false,
                CancellationToken.None);

            act.Should().ThrowAsync<ArgumentException>();
        }
    }
}
