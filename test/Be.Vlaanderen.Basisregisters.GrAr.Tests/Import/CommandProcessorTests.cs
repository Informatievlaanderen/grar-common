namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import
{
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using GrAr.Import.Processing;
    using GrAr.Import.Processing.CommandLine;
    using Infrastructure;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;

    public class CommandProcessorTests
    {
        private readonly ILogger _logger;

        public CommandProcessorTests(ITestOutputHelper output) => _logger = new TestLogger(output, LogLevel.Trace);

        [Theory]
        [InlineData(100, 25, 5, 20, 100, 1, 5, 5)]
        [InlineData(1000, 24, 5, 50, 100, 5, 100, 10)]
        //[InlineData(1000, 50, 100, 1000, 5, 100, 10)]
        public void ProcessesAllKeys(int nrOfKeys,
            int nrOfCommandsPerKey,
            int avgDurationGenerateCommandsForKey,
            int batchSize,
            int avgDurationPostBatch,
            int bufferSize,
            int nrOfConsumers,
            int nrOfProducers)
        {
            var proxyFactory = new TestApiProxyFactory(
                _logger,
                avgDurationPostBatch,
                proxy => proxy.ConfigureInitialise<int>(
                    (importOptions, configuration) =>
                        new CommandProcessorOptions<int>(
                            DateTime.MinValue,
                            DateTime.Now,
                            null,
                            null,
                            false,
                            ImportMode.Init)));

            var options = new InitArguments().ToImportOptions();
            var config = new DefaultCommandProcessorConfig() { BufferSize = bufferSize, NrOfConsumers = nrOfConsumers, NrOfProducers = nrOfProducers, BatchSize = batchSize };
            var generator = new TestCommandGenerator(nrOfKeys, nrOfCommandsPerKey, avgDurationGenerateCommandsForKey);
            var filename = $"processedKeys_{Guid.NewGuid()}.log";
            var processedKeys = new ConcurrentFileBasedProcessedKeysSet<int>(i => i.ToString(), int.Parse, filename);

            var processor = new CommandProcessor<int>(
                config,
                generator,
                processedKeys,
                proxyFactory,
                _logger,
                JsonSerializer.Create());

            processor.Run(options, new TestBatchConfiguration<int>());

            proxyFactory.AllImportedKeys().Should().HaveCount(nrOfKeys);
            proxyFactory.AllImportedKeys().Should().OnlyHaveUniqueItems();
            var keys = File.ReadAllLines(filename);
            keys.Should().HaveCount(nrOfKeys);
            keys.Should().OnlyHaveUniqueItems();
            File.Delete(filename);
        }

        [Fact]
        public void CanPerformCleanStart()
        {
            var processedKeys = new Mock<IProcessedKeysSet<int>>();
            processedKeys.WhenContainsNothing();

            var builder = new CommandProcessorBuilder<int>(new TestCommandGenerator())
                .UseDefaultTestConfiguration(
                    _logger,
                    proxy => proxy.ConfigureInitialise<int>(
                        (importOptions, configuration) =>
                            new CommandProcessorOptions<int>(
                            DateTime.MinValue,
                            DateTime.MaxValue,
                            null,
                            null,
                            true,
                            ImportMode.Init))
                )
                .UseProcessedKeysSet(processedKeys.Object);

            var options = new InitArguments().ToImportOptions();
;
            builder
                .Build()
                .Run(options, new TestBatchConfiguration<int>());

            processedKeys.ThenWasCleared();
        }

        [Fact]
        public void CanSpecifyKeys()
        {
            var processedKeys = new TestProcessedKeysSet();
            var nrOfKeys = 10;
            var keys = Enumerable.Range(100, nrOfKeys).ToList();

            var commandProcessor = new CommandProcessorBuilder<int>(new TestCommandGenerator())
                .UseDefaultTestConfiguration(
                    _logger,
                    proxy => proxy.ConfigureInitialise<int>(
                        (importOptions, configuration) =>
                            new CommandProcessorOptions<int>(
                                DateTime.MinValue,
                                DateTime.MaxValue,
                                keys,
                                null,
                                false,
                                ImportMode.Init)))
                .UseProcessedKeysSet(processedKeys)
                .Build();

            var options = new InitArguments().ToImportOptions();
            var batchConfiguration = new TestBatchConfiguration<int>();

            commandProcessor.Run(options, batchConfiguration);

            processedKeys.Keys.Should().HaveCount(nrOfKeys);
            processedKeys.Keys.Should().Contain(keys);
        }
    }

    public static class MockedIProcessedKeysExtensions
    {
        public static Mock<IProcessedKeysSet<int>> WhenContainsNothing(this Mock<IProcessedKeysSet<int>> mock)
        {
            mock.Setup(x => x.Contains(It.IsAny<int>())).Returns(false);

            return mock;
        }

        public static Mock<IProcessedKeysSet<int>> ThenWasCleared(this Mock<IProcessedKeysSet<int>> mock)
        {
            mock.Verify(x=>x.Clear(), Times.Once);

            return mock;
        }
    }

    public static class CommandProcessorBuilderExtensions
    {
        public static CommandProcessorBuilder<int> UseDefaultTestConfiguration(
            this CommandProcessorBuilder<int> builder,
            ILogger logger,
            Action<TestApiProxy> configureApiProxy)
        {
            builder.UseLoggerFactory(new LoggerFactory(new[] { new LoggerProvider(logger) }));
            builder.UseApiProxyFactory(new TestApiProxyFactory(logger, 100, configureApiProxy));
            builder.UseCommandProcessorConfig(new DefaultCommandProcessorConfig() { BatchSize = 4, BufferSize = 2, NrOfConsumers = 2, NrOfProducers = 5 });
            builder.UseProcessedKeysSet(new TestProcessedKeysSet());

            return builder;
        }
    }

    public class LoggerProvider : ILoggerProvider
    {
        private ILogger _logger;
        public LoggerProvider(ILogger logger)
        {
            _logger = logger;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName) => _logger;
    }
}
