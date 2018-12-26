namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Xunit
{
    using Newtonsoft.Json;
    using Processing.Api;

    public class UnitTestGeneratorFactory : IApiProxyFactory
    {
        private readonly IUnitTestGeneratorConfig _config;
        private readonly JsonSerializer _serializer;

        public UnitTestGeneratorFactory(IUnitTestGeneratorConfig config,
            JsonSerializer serializer)
        {
            _config = config;
            _serializer = serializer;
        }

        public IApiProxy Create() => new UnitTestGenerator(_serializer, _config);
    }
}