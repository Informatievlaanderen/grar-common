namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    public class TestLogger : ILogger
    {
        private readonly LogLevel _minLogLevel;
        private readonly ITestOutputHelper _output;

        public TestLogger(ITestOutputHelper output,
            LogLevel minLogLevel = LogLevel.Trace)
        {
            _minLogLevel = minLogLevel;
            _output = output;
        }

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
                _output.WriteLine($"{DateTime.Now:hh:mm:ss:fff} {logLevel,-12}: {formatter(state, exception)}");
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLogLevel;

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();
    }
}
