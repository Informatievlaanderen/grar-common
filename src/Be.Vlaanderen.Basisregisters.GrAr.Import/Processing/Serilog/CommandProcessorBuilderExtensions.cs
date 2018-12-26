namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Serilog
{
    using System;
    using global::Serilog;
    using global::Serilog.Events;
    using Microsoft.Extensions.Logging;

    public static class CommandProcessorBuilderExtensions
    {
        public static CommandProcessorBuilder<T> UseSerilog<T>(this CommandProcessorBuilder<T> builder,
            Action<LoggerConfiguration> configureSinks)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(Map(builder.MinLogLevel));
            configureSinks?.Invoke(loggerConfiguration);

            var factory = new LoggerFactory();
            factory.AddSerilog(loggerConfiguration.CreateLogger(), true);

            return builder.UseLoggerFactory(factory);
        }

        private static LogEventLevel Map(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return LogEventLevel.Fatal;
                case LogLevel.Error:
                    return LogEventLevel.Error;
                case LogLevel.Warning:
                    return LogEventLevel.Warning;
                case LogLevel.Information:
                    return LogEventLevel.Information;
                case LogLevel.Debug:
                    return LogEventLevel.Debug;
                case LogLevel.Trace:
                    return LogEventLevel.Verbose;
                default:
                    return LogEventLevel.Information;
            }
        }
    }
}
