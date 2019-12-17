namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Api;
    using Consume;
    using Generate;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class CommandProcessor<TKey>
    {
        private readonly IApiProxyFactory _apiProxyFactory;
        private readonly ICommandProcessorConfig _config;
        private readonly ICommandGenerator<TKey> _generator;
        private readonly ILogger _logger;
        private readonly IProcessedKeysSet<TKey> _processedKeys;
        private readonly JsonSerializer _serializer;

        public CommandProcessor(
            ICommandProcessorConfig config,
            ICommandGenerator<TKey> generator,
            IProcessedKeysSet<TKey> processedKeys,
            IApiProxyFactory apiProxyFactory,
            ILogger logger,
            JsonSerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
            _apiProxyFactory = apiProxyFactory;
            _processedKeys = processedKeys;
            _generator = generator;
            _config = config;
        }

        public void Run(
            ImportOptions importOptions,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            var api = _apiProxyFactory.Create();

            var options = api.InitializeImport(importOptions, configuration);
            Run(options);
            api.FinalizeImport(options);
        }

        private void Run(ICommandProcessorOptions<TKey> options)
        {
            _logger.LogInformation("Running {generatorName}", _generator.Name);
            _logger.LogInformation("with options:{options}", options);
            _logger.LogDebug("and config:{config}", _config);

            if (options.CleanStart)
            {
                _logger.LogInformation("Deleting processed keys");
                _processedKeys.Clear();
            }

            var progress = new ProgressTracker(i => _logger.LogInformation("{generatorName}: {progress:000}% processed", _generator.Name, i));

            using (var bus = new Bus.Bus(_config.BufferSize))
            {
                var consumers = SubscribeConsumers(bus);

                var keys = options.Keys != null && options.Keys.Any()
                    ? options.Keys.ToList()
                    : GetKeys(options);

                progress.Total = keys.Count;
                _logger.LogInformation("Processing {keysToProcessCount} keys", progress.Total);

                Parallel.ForEach(
                    keys,
                    new ParallelOptions
                    {
                        MaxDegreeOfParallelism = _config.NrOfProducers //_processConfig.MaxDegreeOfParallelism, CancellationToken = _cts.Token//TODO: add cancellation support
                    },
                    key =>
                    {
                        var import = CreateKeyImport(key, options);
                        bus.ProduceAsync(import).ContinueWith(t => progress++).Wait();
                    });

                _logger.LogInformation("Waiting for buffer depletion");
                bus.Complete().Wait();

                _logger.LogInformation("Flushing consumers");
                FlushConsumers(consumers);
            }
        }

        private KeyImport<TKey> CreateKeyImport(
            TKey key,
            ICommandProcessorOptions<TKey> options)
        {
            var stopwatchGenerate = Stopwatch.StartNew();

            var commands = options.Mode == ImportMode.Init
                ? _generator.GenerateInitCommandsFor(key, options.From, options.Until)
                : _generator.GenerateUpdateCommandsFor(key, options.From, options.Until);

            stopwatchGenerate.Stop();

            var stopwatchSerialize = Stopwatch.StartNew();

            var keyImport = new KeyImport<TKey>(
                key,
                commands.Select(c =>
                    new ImportCommand
                    {
                        Type = c.GetType().FullName,
                        CrabItem = _serializer.Serialize((object) c)
                    }).ToArray());

            stopwatchSerialize.Stop();

            _logger.LogDebug(
                "{producerId} Generated {commandCount} commands for key {key}, generation took ({generateDuration}) and serialization took ({serializationDuration})",
                Thread.CurrentThread.ManagedThreadId,
                keyImport.Commands.Length,
                keyImport.Key,
                stopwatchGenerate.ElapsedMilliseconds,
                stopwatchSerialize.ElapsedMilliseconds);

            return keyImport;
        }

        private void FlushConsumers(IEnumerable<BatchedKeyImportConsumer<TKey>> consumers)
        {
            foreach (var consumer in consumers)
                consumer.Flush();
        }

        private IEnumerable<BatchedKeyImportConsumer<TKey>> SubscribeConsumers(Bus.Bus bus)
        {
            var consumers = new List<BatchedKeyImportConsumer<TKey>>();
            for (var i = 0; i < _config.NrOfConsumers; i++)
            {
                var consumer = new BatchedKeyImportConsumer<TKey>(_logger, _processedKeys, _apiProxyFactory.Create(), _config.BatchSize);
                bus.SubscribeConsumer<KeyImport<TKey>>(consumer.Handle);
                consumers.Add(consumer);
            }

            return consumers;
        }

        private IList<TKey> GetKeys(ICommandProcessorOptions<TKey> options)
        {
            var keys = _generator.GetChangedKeys(options.From, options.Until).ToList();
            var keyCount = keys.Count();
            _logger.LogInformation("{generatorName} found {keyCount} keys", _generator.Name, keyCount);

            keys = keys.Where(key => !_processedKeys.Contains(key)).ToList();
            _logger.LogInformation("Skipping {processedKeysCount} processed keys", keyCount - keys.Count);

            if (options.Take.HasValue)
            {
                _logger.LogInformation("Taking first {take} keys", options.Take.Value);
                keys = keys.Take(options.Take.Value).ToList();
            }

            return keys;
        }

        private class ProgressTracker
        {
            private readonly Action<int> _progressChanged;
            private int _current;

            private int _progress;

            public int Total { get; set; }

            public ProgressTracker(Action<int> progressChanged) => _progressChanged = progressChanged;

            public static ProgressTracker operator ++(ProgressTracker tracker)
            {
                Interlocked.Increment(ref tracker._current);
                tracker.RecalculateProgress();
                return tracker;
            }

            private void RecalculateProgress()
            {
                var progress = (int) ((double) _current / Total * 100.0);
                if (progress != _progress)
                {
                    _progress = progress;
                    _progressChanged(progress);
                }
            }
        }
    }
}
