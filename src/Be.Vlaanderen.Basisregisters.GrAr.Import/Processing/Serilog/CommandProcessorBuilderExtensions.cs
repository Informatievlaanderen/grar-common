namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Serilog
{
    using System;
    using System.Collections.Generic;
    using global::Serilog;
    using global::Serilog.Events;
    using Microsoft.Extensions.Logging;

    public static class CommandProcessorBuilderExtensions
    {
        public static CommandProcessorBuilder<T> UseSerilog<T>(
            this CommandProcessorBuilder<T> builder,
            Action<LoggerConfiguration> configureSinks)
            where T : notnull
        {
            var minimumLogLevel = builder.MinLogLevel;
            var logEventLevel = LogLevelMappings.ContainsKey(minimumLogLevel) ? LogLevelMappings[minimumLogLevel] : LogEventLevel.Information;

            var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Is(logEventLevel);
            configureSinks.Invoke(loggerConfiguration);

            var factory = new LoggerFactory();
            factory.AddSerilog(loggerConfiguration.CreateLogger(), true);

            return builder.UseLoggerFactory(factory);
        }

        private static IReadOnlyDictionary<LogLevel, LogEventLevel> LogLevelMappings =>
            new Dictionary<LogLevel, LogEventLevel>
            {
                {LogLevel.Critical, LogEventLevel.Fatal},
                {LogLevel.Error, LogEventLevel.Error},
                {LogLevel.Warning, LogEventLevel.Warning},
                {LogLevel.Information, LogEventLevel.Information},
                {LogLevel.Debug, LogEventLevel.Debug},
                {LogLevel.Trace, LogEventLevel.Verbose},
            };
    }
}
