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
        private readonly IDictionary<Type, dynamic> _initializeBehaviours = new Dictionary<Type, dynamic>();

        public TestApiProxy(ILogger logger,
            int id,
            int averageDuration)
        {
            _averageDuration = averageDuration;
            _id = id;
            _logger = logger;
            _batches = new List<IEnumerable<int>>();
        }

        private void Trace(string message) => _logger.LogTrace($"TESTAPIPROXY {_id} {message}");

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            var keysArray = imports.Select(x => x.Key).ToArray();
            var keys = string.Join(", ", keysArray);
            Trace($"Posting {keys}");
            Thread.Sleep(_averageDuration);
            Trace($"Posted {keys}");
            _batches.Add(keysArray.Cast<int>());
        }

        public ICommandProcessorOptions<TKey> InitializeImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            var type = typeof(TKey);
            if (_initializeBehaviours.ContainsKey(type))
                return _initializeBehaviours[type](options, configuration);

            throw new Exception($"{nameof(InitializeImport)} is not configured for {type}");
        }

        public void ConfigureInitialize<TKey>(Func<ImportOptions, ICommandProcessorBatchConfiguration<TKey>, ICommandProcessorOptions<TKey>> behavior)
            => _initializeBehaviours[typeof(TKey)] = behavior;

        public void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options)
            => Trace("Finalizing");

        public IEnumerable<int> AllImportedKeys()
            => _batches.SelectMany(x => x);
    }
}
