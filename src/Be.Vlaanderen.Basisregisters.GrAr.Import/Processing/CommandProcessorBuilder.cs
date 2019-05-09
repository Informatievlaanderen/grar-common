namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using Api;
    using Generate;
    using Json;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Reflection;

    public class CommandProcessorBuilder<TKey>
    {
        private readonly ICommandGenerator<TKey> _generator;
        private IApiProxyFactory _apiProxyFactory;
        private ICommandProcessorConfig _commandProcessorConfig;
        private IHttpApiProxyConfig _httpApiProxyConfig;
        private LoggerFactory _loggerFactory;
        private IProcessedKeysSet<TKey> _processedKeys;
        private JsonSerializerSettings _serializerSettings;
        private bool _useDryRunApiProxyFactory;
        private ICommandProcessorOptions<TKey> _options;

        public ICommandProcessorOptions<TKey> Options
            => _options ?? throw new CommandProcessorBuilderConfigurationException("No CommandProcessorOptions was set. Call UseCommandProcessorOptions to set options");

        public LogLevel MinLogLevel { get; private set; }

        public CommandProcessorBuilder(ICommandGenerator<TKey> generator) => _generator = generator ?? throw new ArgumentNullException(nameof(generator));

        public CommandProcessorBuilder<TKey> SetCommandProcessorOptions(ICommandProcessorOptions<TKey> options)
        {
            _options = options;

            return this;
        }

        public CommandProcessorBuilder<TKey> SetMinLogLevel(LogLevel minLogLevel)
        {
            MinLogLevel = minLogLevel;

            return this;
        }

        public CommandProcessorBuilder<TKey> UseCommandProcessorConfig(ICommandProcessorConfig config)
        {
            _commandProcessorConfig = config;

            return this;
        }

        public CommandProcessorBuilder<TKey> UseHttpApiProxyConfig(IHttpApiProxyConfig config)
        {
            _httpApiProxyConfig = config;

            return this;
        }

        public CommandProcessorBuilder<TKey> UseProcessedKeysSet(IProcessedKeysSet<TKey> processedKeys)
        {
            _processedKeys = processedKeys;

            return this;
        }

        public CommandProcessorBuilder<TKey> UseApiProxyFactory(IApiProxyFactory factory)
        {
            _apiProxyFactory = factory;
            return this;
        }

        public CommandProcessorBuilder<TKey> UseLoggerFactory(LoggerFactory factory)
        {
            _loggerFactory = factory;
            return this;
        }

        public CommandProcessorBuilder<TKey> UseDryRunApiProxyFactory()
        {
            _useDryRunApiProxyFactory = true;
            return this;
        }

        public CommandProcessorBuilder<TKey> ConfigureProcessedKeySerialization(Func<TKey, string> serializeKey,
            Func<string, TKey> deserializeKey)
        {
            _processedKeys = new ConcurrentFileBasedProcessedKeysSet<TKey>(serializeKey, deserializeKey);

            return this;
        }

        public CommandProcessorBuilder<TKey> UseDefaultSerializerSettingsForCrabImports()
        {
            _serializerSettings = new JsonSerializerSettings().ConfigureForCrabImports();

            return this;
        }

        public CommandProcessorBuilder<TKey> ConfigureSerializerSettings(Action<JsonSerializerSettings> configure)
        {
            _serializerSettings = new JsonSerializerSettings();
            configure(_serializerSettings);

            return this;
        }

        public CommandProcessor<TKey> Build()
        {
            var logger = _loggerFactory?.CreateLogger(Assembly.GetExecutingAssembly().FullName) ?? throw new CommandProcessorBuilderConfigurationException("No LoggerFactory was set. Call UseLoggerFactory to set a factory");

            var config = _commandProcessorConfig ?? new DefaultCommandProcessorConfig();
            var processedKeys = _processedKeys ?? new ConcurrentFileBasedProcessedKeysSet<TKey>(x => x.ToString(), s => (TKey)Convert.ChangeType(s, typeof(TKey)));
            var serializer = JsonSerializer.CreateDefault(_serializerSettings);

            return new CommandProcessor<TKey>(config,
                _generator,
                processedKeys,
                GetApiProxyFactory(logger, serializer),
                logger,
                serializer);
        }

        private IApiProxyFactory GetApiProxyFactory(ILogger logger, JsonSerializer serializer)
        {
            if (_apiProxyFactory != null)
                return _apiProxyFactory;

            if(logger == null)
                throw new ArgumentNullException(nameof(logger));

            if (_useDryRunApiProxyFactory)
                return new DryRunApiProxyFactory(logger);

            if(serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            return new SingleEndpointHttpApiProxyFactory(
                logger,
                serializer,
                _httpApiProxyConfig ?? throw new CommandProcessorBuilderConfigurationException("No IHttpApiProxyConfig was set. Call UseHttpApiProxyConfig or UseApiProxyFactory to set your own factory"));
        }
    }
}
