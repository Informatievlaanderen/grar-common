namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using Api;
    using Generate;
    using Json;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Reflection;
    using CommandLine;

    public class CommandProcessorBuilder<TKey>
    {
        private readonly ICommandGenerator<TKey> _generator;
        private Func<ILogger, IApiProxyFactory> _createApiProxyFactory;
        private ICommandProcessorConfig _commandProcessorConfig;
        private IHttpApiProxyConfig _httpApiProxyConfig;
        private LoggerFactory _loggerFactory;
        private IProcessedKeysSet<TKey> _processedKeys;
        private JsonSerializerSettings _serializerSettings;
        private bool _useDryRunApiProxyFactory;
        private ImportFeed _importFeed;

        public LogLevel MinLogLevel { get; private set; }

        public CommandProcessorBuilder(ICommandGenerator<TKey> generator)
            => _generator = generator ?? throw new ArgumentNullException(nameof(generator));

        public CommandProcessorBuilder<TKey> WithCommandLineOptions<TOptions>(TOptions options)
            where TOptions : ImportArguments
        {
            SetMinLogLevel(options.LogLevel);
            if (options.DryRun)
                UseDryRunApiProxyFactory();

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
            => UseApiProxyFactory(logger => factory);

        public CommandProcessorBuilder<TKey> UseApiProxyFactory(Func<ILogger, IApiProxyFactory> factoryBuilder)
        {
            _createApiProxyFactory = factoryBuilder;

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

        public CommandProcessorBuilder<TKey> ConfigureProcessedKeySerialization(
            Func<TKey, string> serializeKey,
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

        /// <summary>Uses the executing assembly name as import feed.</summary>
        public CommandProcessorBuilder<TKey> ConfigureDefaultImportFeed()
            => ConfigureImportFeed(new ImportFeed{ Name = Assembly.GetExecutingAssembly().GetName().Name });

        public CommandProcessorBuilder<TKey> ConfigureImportFeed(ImportFeed feed)
        {
            _importFeed = feed;

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

        private IApiProxyFactory GetApiProxyFactory(
            ILogger logger,
            JsonSerializer serializer)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            if (_createApiProxyFactory != null)
                return _createApiProxyFactory(logger);

            if (_useDryRunApiProxyFactory)
                return new DryRunApiProxyFactory(logger);

            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            return new SingleEndpointHttpApiProxyFactory(
                logger,
                serializer,
                _httpApiProxyConfig ?? throw new CommandProcessorBuilderConfigurationException("No IHttpApiProxyConfig was set. Call UseHttpApiProxyConfig or UseApiProxyFactory to set your own factory"),
                _importFeed ?? throw new CommandProcessorBuilderConfigurationException($"ImportFeed is not configured. Call {nameof(ConfigureDefaultImportFeed)} or use {nameof(ConfigureImportFeed)} to set custom import feed"));
        }
    }
}
