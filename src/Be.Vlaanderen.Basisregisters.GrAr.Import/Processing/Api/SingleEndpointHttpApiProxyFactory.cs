namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class SingleEndpointHttpApiProxyFactory : IApiProxyFactory
    {
        // TODO: work with HttpClientFactory? https://www.stevejgordon.co.uk/introduction-to-httpclientfactory-aspnetcore
        private readonly IHttpApiProxyConfig _config;
        private readonly ILogger _logger;
        private readonly JsonSerializer _serializer;
        private readonly ImportFeed _importFeed;

        public SingleEndpointHttpApiProxyFactory(
            ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config,
            ImportFeed importFeed)
        {
            _config = config;
            _serializer = serializer;
            _logger = logger;
            _importFeed = importFeed;
        }

        public IApiProxy Create() => new HttpApiProxy(_logger, _serializer, _config, _importFeed);
    }
}
