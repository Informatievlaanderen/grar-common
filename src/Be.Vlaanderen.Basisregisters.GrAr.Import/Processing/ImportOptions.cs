namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Api.Messages;
    using CommandLine;
    using global::CommandLine;
    using NodaTime;
    using NodaTime.Extensions;

    public class ImportOptions
    {
        private readonly ParserResult<object> _parsed;
        private readonly Func<Instant> _getCurrentTimeStamp;

        public ImportOptions(IEnumerable<string> args, Action<IEnumerable<Error>> onNotParsed)
            : this(args, onNotParsed, null)
        { }

        public ImportOptions(
            IEnumerable<string> args,
            Action<IEnumerable<Error>>? onNotParsed,
            Func<Instant>? getCurrentTimeStamp)
        {
            _getCurrentTimeStamp = getCurrentTimeStamp ?? (() => DateTimeOffset.Now.ToInstant());
            _parsed = Parser
                .Default
                .ParseArguments<InitArguments, UpdateArguments>(args)
                .WithNotParsed(onNotParsed ?? (errors => { }));
        }

        public ImportArguments ImportArguments => _parsed.MapResult(
            options => (ImportArguments)options,
            errors => ThrowNotParsed<ImportArguments>(new ApplicationException($"Parsed options expected to have base type {nameof(ImportArguments)}")));

        public ICommandProcessorOptions<TKey> CreateProcessorOptions<TKey>(
            BatchStatus? lastBatch,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            var defaultUntil = _getCurrentTimeStamp().Minus(Duration.FromTimeSpan(configuration.TimeMargin));
            if (lastBatch != null && lastBatch.Until == DateTimeOffset.MinValue)
            {
                lastBatch = null;
            }

            return _parsed.MapResult(
                (InitArguments init) =>
                {
                    if (lastBatch?.Completed ?? false)
                    {
                        throw new InvalidOperationException("Cannot initialize for an already initialized import");
                    }

                    return new CommandProcessorOptions<TKey>(
                        (lastBatch?.From ?? DateTimeOffset.MinValue).ToInstant(),
                        (lastBatch == null || lastBatch.Completed) ? defaultUntil : lastBatch.Until.ToInstant(),
                        ImportArguments.Keys.Select(configuration.Deserialize),
                        init.Take,
                        ImportArguments.CleanStart,
                        ImportMode.Init);
                },
                (UpdateArguments update) =>
                {
                    if (lastBatch == null)
                    {
                        throw new InvalidOperationException("Cannot update an uninitialized import");
                    }

                    return new CommandProcessorOptions<TKey>(
                        lastBatch.Completed ? lastBatch.Until.ToInstant() : lastBatch.From.ToInstant(),
                        lastBatch.Completed ? (update.UntilDateTimeOffset?.ToInstant() ?? defaultUntil) : lastBatch.Until.ToInstant(),
                        ImportArguments.Keys.Select(configuration.Deserialize),
                        null,
                        ImportArguments.CleanStart || lastBatch.Completed,
                        ImportMode.Update);
                },
                errors => ThrowNotParsed<ICommandProcessorOptions<TKey>>(new NotImplementedException($"Create Processor Options has no implementation for {nameof(_parsed.TypeInfo.Current.FullName)}")));
        }

        private TExpected ThrowNotParsed<TExpected>(Exception exception)
        {
            if (_parsed.Tag == ParserResultType.NotParsed)
            {
                throw new InvalidOperationException("No valid command line arguments");
            }

            throw exception;
        }
    }
}
