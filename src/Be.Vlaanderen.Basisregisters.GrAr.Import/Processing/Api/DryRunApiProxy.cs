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
    }
}
