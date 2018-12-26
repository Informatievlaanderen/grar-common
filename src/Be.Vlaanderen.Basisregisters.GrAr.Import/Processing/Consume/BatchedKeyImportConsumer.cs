namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Consume
{
    using System.Collections.Generic;
    using System.Linq;
    using Api;
    using Microsoft.Extensions.Logging;

    internal class BatchedKeyImportConsumer<TKey>
    {
        private readonly int _batchSize;
        private readonly ILogger _logger;
        private readonly IProcessedKeysSet<TKey> _processedKeys;
        private readonly IApiProxy _proxy;
        private List<KeyImport<TKey>> _batch;

        public BatchedKeyImportConsumer(ILogger logger,
            IProcessedKeysSet<TKey> processedKeys,
            IApiProxy proxy,
            int batchSize = 1000)
        {
            _logger = logger;
            _proxy = proxy;
            _batchSize = batchSize;
            _processedKeys = processedKeys;
            _batch = new List<KeyImport<TKey>>(batchSize);
        }

        public virtual void Handle(KeyImport<TKey> message)
        {
            _batch.Add(message);

            var currentBatchSize = _batch.Sum(x => x.Commands.Length);
            if (currentBatchSize >= _batchSize)
            {
                _logger.LogDebug("{ConsumerId} Flushing batch of {currentBatchSize} commands for {keyCount} keys", GetHashCode(), currentBatchSize, _batch.Count);
                Flush();
            }

            _logger.LogTrace("Handled import for {key}", message.Key);
        }

        public void Flush()
        {
            if (_batch.Any())
            {
                _proxy.ImportBatch(_batch);
                _processedKeys.Add(_batch.Select(m => m.Key));
                _batch = new List<KeyImport<TKey>>();
            }
        }
    }
}
