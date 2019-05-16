namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System;
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
        private readonly Action<TestApiProxy> _configure;

        public TestApiProxyFactory(
            ILogger logger,
            int averageDuration,
            Action<TestApiProxy> configure)
        {
            _averageDuration = averageDuration;
            _logger = logger;
            _proxies = new List<TestApiProxy>();
            _configure = configure ?? (proxy => {});
        }

        public IApiProxy Create()
        {
            var proxy = new TestApiProxy(_logger, _counter++, _averageDuration);
            _configure(proxy);
            _proxies.Add(proxy);

            return proxy;
        }

        public IEnumerable<int> AllImportedKeys()
        {
            return _proxies.SelectMany(x => x.AllImportedKeys());
        }
    }
}
