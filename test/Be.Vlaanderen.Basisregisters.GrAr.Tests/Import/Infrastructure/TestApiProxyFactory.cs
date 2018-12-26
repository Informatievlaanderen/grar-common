namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using GrAr.Import.Processing.Api;
    using Microsoft.Extensions.Logging;

    public class TestApiProxyFactory : IApiProxyFactory
    {
        private readonly int _averageDuration;
        private readonly ILogger _logger;
        private readonly List<TestApiProxy> _proxies;
        private int _counter;

        public TestApiProxyFactory(ILogger logger,
            int averageDuration = 100)
        {
            _averageDuration = averageDuration;
            _logger = logger;
            _proxies = new List<TestApiProxy>();
        }

        public IApiProxy Create()
        {
            var proxy = new TestApiProxy(_logger, _counter++, _averageDuration);
            _proxies.Add(proxy);

            return proxy;
        }

        public IEnumerable<int> AllImportedKeys()
        {
            return _proxies.SelectMany(x => x.AllImportedKeys());
        }
    }
}
