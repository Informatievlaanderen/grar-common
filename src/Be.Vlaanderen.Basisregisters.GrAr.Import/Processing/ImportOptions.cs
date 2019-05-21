namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;
    using global::CommandLine;

    public class ImportOptions
    {
        private readonly ParserResult<object> _parsed;
        private readonly Func<DateTime> _getCurrentTimeStamp;

        public ImportOptions(IEnumerable<string> args, Action<IEnumerable<Error>> onNotParsed)
            : this(args, onNotParsed, null)
        { }

        public ImportOptions(
            IEnumerable<string> args,
            Action<IEnumerable<Error>> onNotParsed,
            Func<DateTime> getCurrentTimeStamp)
        {
            _getCurrentTimeStamp = getCurrentTimeStamp ?? (() => DateTime.Now);
            _parsed = Parser
                .Default
                .ParseArguments<InitArguments, UpdateArguments>(args)
                .WithNotParsed(onNotParsed ?? (errors => { }));
        }

        public ImportArguments ImportArguments => _parsed.MapResult(
            options => (ImportArguments)options,
            errors => ThrowNotParsed<ImportArguments>(new ApplicationException($"Parsed options expected to have base type {nameof(ImportArguments)}")));

        public ICommandProcessorOptions<TKey> CreateProcessorOptions<TKey>(
            ImportBatchStatus lastBatch,
            ICommandProcessorBatchConfiguration<TKey> configuration)
        {
            var defaultUntil = _getCurrentTimeStamp().Add(-configuration.TimeMargin);
            if (lastBatch != null && lastBatch.IsInvalid)
                lastBatch = null;

            return _parsed.MapResult(
                (InitArguments init) =>
                {
                    if (lastBatch?.Completed ?? false)
                        throw new ApplicationException("Cannot initialize an for an already initialized import");

                    return new CommandProcessorOptions<TKey>(
                        lastBatch?.From ?? DateTime.MinValue,
                        (lastBatch == null || lastBatch.Completed) ? defaultUntil : lastBatch.Until,
                        ImportArguments.Keys.Select(configuration.Deserialize),
                        init.Take,
                        ImportArguments.CleanStart,
                        ImportMode.Init);
                },
                (UpdateArguments update) =>
                {
                    if (lastBatch == null)
                        throw new ApplicationException("Cannot update an uninitialized import");

                    return new CommandProcessorOptions<TKey>(
                        lastBatch.Completed ? lastBatch.Until : lastBatch.From,
                        lastBatch.Completed ? (update.Until ?? defaultUntil) : lastBatch.Until,
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
                throw new ApplicationException("No valid command line arguments");
            throw exception;
        }
    }
}
