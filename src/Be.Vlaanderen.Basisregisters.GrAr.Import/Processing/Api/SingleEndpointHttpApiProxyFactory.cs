namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class SingleEndpointHttpApiProxyFactory : IApiProxyFactory
    {
        //TODO: work with HttpClientFactory? https://www.stevejgordon.co.uk/introduction-to-httpclientfactory-aspnetcore
        private readonly IHttpApiProxyConfig _config;
        private readonly ILogger _logger;
        private readonly JsonSerializer _serializer;

        public SingleEndpointHttpApiProxyFactory(
            ILogger logger,
            JsonSerializer serializer,
            IHttpApiProxyConfig config)
        {
            _config = config;
            _serializer = serializer;
            _logger = logger;
        }

        public IApiProxy Create() => new HttpApiProxy(_logger, _serializer, _config);
    }
}
