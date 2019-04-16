namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Commandline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;

    public static class CommandProcessorBuilderExtensions
    {
        public static CommandProcessorBuilder<TKey> UseCommandLineArgs<TKey>(
            this CommandProcessorBuilder<TKey> builder,
            string[] args,
            DateTime? failOverFrom,
            DateTime? failOverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey,
            Action<IEnumerable<Error>> onParserError = null)
        {
            var parserResult = Parser.Default.ParseArguments<InitOptions, UpdateOptions>(args);
            if (onParserError != null)
                parserResult.WithNotParsed(onParserError);

            parserResult
                .WithParsed<InitOptions>(options => builder = builder.UseInitOptions(options, failOverUntil, timeMargin, deserializeKey))
                .WithParsed<UpdateOptions>(options => builder = builder.UseUpdateOptions(options, failOverFrom, failOverUntil, timeMargin, deserializeKey));

            return builder;
        }

        public static CommandProcessorBuilder<TKey> UseInitOptions<TKey>(
            this CommandProcessorBuilder<TKey> builder,
            InitOptions options,
            DateTime? failOverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey)
        {
            var commandProcessorOptions =
                new CommandProcessorOptions<TKey>(
                    DateTime.MinValue,
                    failOverUntil ?? DateTime.Now.Add(-timeMargin),
                    options.Keys.Select(deserializeKey),
                    options.Take,
                    options.CleanStart,
                    ImportMode.Init);

            if (options.DryRun)
                builder = builder.UseDryRunApiProxyFactory();

            builder.SetMinLogLevel(options.LogLevel);

            return builder.SetCommandProcessorOptions(commandProcessorOptions);
        }

        public static CommandProcessorBuilder<TKey> UseUpdateOptions<TKey>(
            this CommandProcessorBuilder<TKey> builder,
            UpdateOptions options,
            DateTime? failOverFrom,
            DateTime? failOverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey)
        {
            var commandProcessorOptions = new CommandProcessorOptions<TKey>(
                options.From ?? failOverFrom ?? DateTime.MinValue,
                options.Until ?? failOverUntil ?? DateTime.Now.Add(-timeMargin),
                options.Keys.Select(deserializeKey),
                null,
                options.CleanStart,
                ImportMode.Update);

            if (options.DryRun)
                builder = builder.UseDryRunApiProxyFactory();

            builder.SetMinLogLevel(options.LogLevel);

            return builder.SetCommandProcessorOptions(commandProcessorOptions);
        }
    }
}
