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
                .WithNotParsed(onNotParsed ?? (errors => {}));
        }

        public ImportArguments ImportArguments => _parsed.MapResult(
            options => (ImportArguments)options,
            errors => ThrowNotParsed<ImportArguments>(new ApplicationException($"Parsed options expected to have base type {nameof(ImportArguments)}")));

        public ICommandProcessorOptions<TKey> CreateProcessorOptions<TKey>(
            DateTime? lastCompletedImport,
            DateTime? recoverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey)
        {
            var defaultUntil = _getCurrentTimeStamp().Add(-timeMargin);

            return _parsed.MapResult(
                (InitArguments init) => new CommandProcessorOptions<TKey>(
                    lastCompletedImport ?? DateTime.MinValue,
                    recoverUntil ?? defaultUntil,
                    ImportArguments.Keys.Select(deserializeKey),
                    init.Take,
                    ImportArguments.CleanStart,
                    ImportMode.Init),
                (UpdateArguments update) => new CommandProcessorOptions<TKey>(
                    lastCompletedImport ?? throw new ApplicationException("Cannot update an uninitialized import"),
                    recoverUntil ?? update.Until ?? defaultUntil,
                    ImportArguments.Keys.Select(deserializeKey),
                    null,
                    ImportArguments.CleanStart || !recoverUntil.HasValue,
                    ImportMode.Update),
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
