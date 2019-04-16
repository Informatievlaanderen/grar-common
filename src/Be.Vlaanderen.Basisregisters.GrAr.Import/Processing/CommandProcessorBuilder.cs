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

        public ICommandProcessorOptions<TKey> Options { get; private set; }
        public LogLevel MinLogLevel { get; private set; }

        public CommandProcessorBuilder(ICommandGenerator<TKey> generator) => _generator = generator ?? throw new ArgumentNullException(nameof(generator));

        public CommandProcessorBuilder<TKey> SetCommandProcessorOptions(ICommandProcessorOptions<TKey> options)
        {
            Options = options;

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
            var config = GetCommandProcessorConfig();
            var processedKeys = GetProcessedKeys();
            var apiProxyFactory = GetApiProxyFactory();
            var logger = GetLogger();
            var serializer = GetSerializer();
            var result = new CommandProcessor<TKey>(config,
                _generator,
                processedKeys,
                apiProxyFactory,
                logger,
                serializer
            );

            return result;
        }

        public void BuildAndRun()
        {
            var processor = Build();
            processor.Run(GetOptions());
        }

        private JsonSerializer GetSerializer() => _serializerSettings != null ? JsonSerializer.CreateDefault(_serializerSettings) : JsonSerializer.CreateDefault();

        private ICommandProcessorOptions<TKey> GetOptions()
        {
            if (Options == null)
                throw new CommandProcessorBuilderConfigurationException("No CommandProcessorOptions was set. Call UseCommandProcessorOptions to set options");

            return Options;
        }

        private ILogger GetLogger()
        {
            if (_loggerFactory == null)
                throw new CommandProcessorBuilderConfigurationException("No LoggerFactory was set. Call UseLoggerFactory to set a factory");
            return _loggerFactory.CreateLogger(Assembly.GetExecutingAssembly().FullName);
        }

        private IHttpApiProxyConfig GetHttpApiProxyConfig()
        {
            if (_httpApiProxyConfig == null)
                throw new CommandProcessorBuilderConfigurationException("No IHttpApiProxyConfig was set. Call UseHttpApiProxyConfig or UseApiProxyFactory to set your own factory");
            return _httpApiProxyConfig;
        }

        private IApiProxyFactory GetApiProxyFactory()
        {
            if (_apiProxyFactory != null)
                return _apiProxyFactory;

            if (_useDryRunApiProxyFactory)
                return new DryRunApiProxyFactory(GetLogger());

            return _apiProxyFactory = new SingleEndpointHttpApiProxyFactory(GetLogger(), GetSerializer(), GetHttpApiProxyConfig());
        }

        private IProcessedKeysSet<TKey> GetProcessedKeys()
        {
            if (_processedKeys != null)
                return _processedKeys;

            return new ConcurrentFileBasedProcessedKeysSet<TKey>(x => x.ToString(), s => (TKey)Convert.ChangeType(s, typeof(TKey)));
        }

        private ICommandProcessorConfig GetCommandProcessorConfig()
        {
            if (_commandProcessorConfig != null)
                return _commandProcessorConfig;

            return new DefaultCommandProcessorConfig();
        }
    }
}
