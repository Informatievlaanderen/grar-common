namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using Microsoft.Extensions.Logging;

    public class DryRunApiProxyFactory : IApiProxyFactory
    {
        private readonly ILogger _logger;

        public DryRunApiProxyFactory(ILogger logger) => _logger = logger;

        public IApiProxy Create() => new DryRunApiProxy(_logger);
    }
}
