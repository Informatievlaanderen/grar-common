namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using GrAr.Import.Processing;
    using GrAr.Import.Processing.Api;
    using Microsoft.Extensions.Logging;

    public class TestApiProxy : IApiProxy
    {
        private readonly int _averageDuration;
        private readonly List<IEnumerable<int>> _batches;
        private readonly int _id;
        private readonly ILogger _logger;

        public TestApiProxy(ILogger logger,
            int id,
            int averageDuration = 1000)
        {
            _averageDuration = averageDuration;
            _id = id;
            _logger = logger;
            _batches = new List<IEnumerable<int>>();
        }

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var keysArray = imports.Select(x => x.Key).ToArray();
            var keys = string.Join(", ", keysArray);
            _logger.LogTrace($"TESTAPIPROXY {_id} Posting {keys}");
            Thread.Sleep(_averageDuration);
            _logger.LogTrace($"TESTAPIPROXY {_id} Posted {keys}");
            _batches.Add(keysArray.Cast<int>());
        }

        public ICommandProcessorOptions<TKey> InitialiseImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
            => throw new NotImplementedException();

        public void FinaliseImport<TKey>(ICommandProcessorOptions<TKey> options)
            => throw new NotImplementedException();

        public IEnumerable<int> AllImportedKeys()
            => _batches.SelectMany(x => x);
    }
}
