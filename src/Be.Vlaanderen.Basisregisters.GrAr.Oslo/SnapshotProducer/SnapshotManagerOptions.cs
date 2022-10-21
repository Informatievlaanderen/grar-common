namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo.SnapshotProducer
{
    using System;

    public sealed class SnapshotManagerOptions
    {
        private SnapshotManagerOptions()
        { }

        public int MaxRetryWaitIntervalSeconds { get; private init; }
        public int RetryBackoffFactor { get; private init; }

        public static SnapshotManagerOptions Create(string maxRetryWaitIntervalSeconds, string retryBackoffFactor)
        {
            if (string.IsNullOrEmpty(maxRetryWaitIntervalSeconds) || string.IsNullOrEmpty(retryBackoffFactor))
            {
                throw new ArgumentException("Config settings MaxRetryWaitIntervalSeconds and RetryBackoffFactor need to be set.");
            }

            return new SnapshotManagerOptions
            {
                MaxRetryWaitIntervalSeconds = Convert.ToInt32(maxRetryWaitIntervalSeconds),
                RetryBackoffFactor = Convert.ToInt32(retryBackoffFactor)
            };
        }
    }
}
