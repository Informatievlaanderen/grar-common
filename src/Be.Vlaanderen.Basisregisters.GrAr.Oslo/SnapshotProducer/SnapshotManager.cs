namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using NodaTime;
    using Polly;

    public sealed class SnapshotManager : ISnapshotManager
    {
        private readonly ILogger<SnapshotManager> _logger;
        private readonly IOsloProxy _osloProxy;
        private readonly int _maxRetryWaitIntervalSeconds;
        private readonly int _retryBackoffFactor;

        public SnapshotManager(
            ILoggerFactory loggerFactory,
            IOsloProxy osloProxy,
            SnapshotManagerOptions options)
        {

            _logger = loggerFactory.CreateLogger<SnapshotManager>();
            _osloProxy = osloProxy;
            _maxRetryWaitIntervalSeconds = options.MaxRetryWaitIntervalSeconds;
            _retryBackoffFactor = options.RetryBackoffFactor;
        }

        public async Task<OsloResult?> FindMatchingSnapshot(
            string objectId,
            Instant eventVersion,
            string? eventHash,
            long eventPosition,
            bool throwStaleWhenGone,
            CancellationToken ct)
        {
            return await FindMatchingSnapshot(objectId, eventVersion, eventHash, eventPosition, throwStaleWhenGone, false, ct);
        }

        public async Task<OsloResult?> FindMatchingSnapshot(
            string objectId,
            Instant eventVersion,
            string? eventHash,
            long eventPosition,
            bool throwStaleWhenGone,
            bool matchOnHashOnly,
            CancellationToken ct)
        {
            if (matchOnHashOnly && eventHash is null)
            {
                throw new ArgumentNullException(nameof(eventHash), "An event hash is required when matching on hash only.");
            }

            return await Policy
                .Handle<Exception>(e =>
                    {
                        var shouldHandle = e is StaleSnapshotException or HttpRequestException;

                        if (shouldHandle)
                        {
                            _logger.LogWarning(e, $"Retry getting snapshot for objectId '{objectId}' with event position '{eventPosition}'.");
                        }

                        return shouldHandle;
                    })
                .WaitAndRetryForeverAsync(retryAttempt =>
                {
                    var waitIntervalSeconds = retryAttempt * _retryBackoffFactor;

                    if (waitIntervalSeconds > _maxRetryWaitIntervalSeconds)
                    {
                        waitIntervalSeconds = _maxRetryWaitIntervalSeconds;
                    }

                    return TimeSpan.FromSeconds(waitIntervalSeconds);
                })
                .ExecuteAsync(async _ => await GetSnapshot(), ct);

            async Task<OsloResult?> GetSnapshot()
            {
                if (ct.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                OsloResult? snapshot;

                try
                {
                    _logger.LogInformation($"Requesting snapshot for '{objectId}'");
                    snapshot = await _osloProxy.GetSnapshot(objectId, ct);
                    _logger.LogInformation("Snapshot received.");
                }
                catch (HttpRequestException e)
                {
                    _logger.LogWarning(e, $"HttpRequestException while getting snapshot for objectId '{objectId}' with event position '{eventPosition}'.");

                    switch (e.StatusCode)
                    {
                        case HttpStatusCode.Gone when throwStaleWhenGone: throw;
                        case HttpStatusCode.Gone: return null;
                        default:
                            throw;
                    }
                }

                var snapshotDto = DateTimeOffset.Parse(snapshot.Identificator.Versie);
                var snapshotVersion = Instant.FromDateTimeOffset(snapshotDto);

                var versionDeltaInSeconds = Math.Floor(eventVersion.Minus(snapshotVersion).TotalSeconds);

                if (matchOnHashOnly)
                {
                    if (snapshot.ETag == eventHash)
                        return snapshot;

                    if (versionDeltaInSeconds < 0)
                        return null;

                    if (string.IsNullOrEmpty(snapshot.ETag))
                        return snapshot;

                    throw new StaleSnapshotException();
                }

                if (versionDeltaInSeconds > 0)
                {
                    throw new StaleSnapshotException();
                }

                if (snapshot.ETag is not null && eventHash is not null)
                {
                    return versionDeltaInSeconds == 0 && snapshot.ETag == eventHash
                        ? snapshot
                        : null;
                }

                return versionDeltaInSeconds == 0
                    ? snapshot
                    : null;
            }
        }
    }
}
