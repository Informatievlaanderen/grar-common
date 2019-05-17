namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class DryRunApiProxy : IApiProxy
    {
        private readonly ILogger _logger;

        public DryRunApiProxy(ILogger logger) => _logger = logger;

        public void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports)
        {
            _logger.LogDebug("Fake sending {keyCount} imports", imports.Count());
            _logger.LogTrace("Payload: {@imports}", imports);
        }

        public ICommandProcessorOptions<TKey> InitializeImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration)
            => throw new System.NotImplementedException();

        public void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options)
            => throw new System.NotImplementedException();
    }
}
