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
        private readonly IDictionary<Type, dynamic> _initialiseBehaviours = new Dictionary<Type, dynamic>();

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

        public ICommandProcessorOptions<TKey> InitialiseImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            var type = typeof(TKey);
            if (_initialiseBehaviours.ContainsKey(type))
                return _initialiseBehaviours[type](options, configuration);

            throw new Exception($"{nameof(InitialiseImport)} is not configured for {type}");
        }

        public void ConfigureInitialise<TKey>(Func<ImportOptions, ICommandProcessorBatchConfiguration<TKey>, ICommandProcessorOptions<TKey>> behavior)
            => _initialiseBehaviours[typeof(TKey)] = behavior;

        public void FinaliseImport<TKey>(ICommandProcessorOptions<TKey> options)
            => Trace("Finalising");

        public IEnumerable<int> AllImportedKeys()
            => _batches.SelectMany(x => x);
    }
}
