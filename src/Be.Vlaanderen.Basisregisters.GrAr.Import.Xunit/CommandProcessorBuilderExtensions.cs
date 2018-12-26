namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandLine;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Processing;
    using Processing.Commandline;
    using Processing.Json;

    public static class CommandProcessorBuilderExtensions
    {
        public static CommandProcessorBuilder<TKey> UseCommandLineArgs<TKey>(this CommandProcessorBuilder<TKey> builder,
            string[] args,
            DateTime? failOverFrom,
            DateTime? failOverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey,
            IUnitTestGeneratorConfig unitTestGeneratorConfig,
            Action<IEnumerable<Error>> onParserError = null)
        {
            var parserResult = Parser.Default.ParseArguments<InitOptionsWithUnitTestOption, UpdateOptions>(args);
            if (onParserError != null)
                parserResult.WithNotParsed(onParserError);

            parserResult
                .WithParsed<InitOptionsWithUnitTestOption>(options => builder = builder.UseInitOptionsWithUnitTestOption(options, failOverUntil, timeMargin, deserializeKey, unitTestGeneratorConfig))
                .WithParsed<UpdateOptions>(options => builder = builder.UseUpdateOptions(options, failOverFrom, failOverUntil, timeMargin, deserializeKey));

            return builder;
        }

        public static CommandProcessorBuilder<TKey> UseInitOptionsWithUnitTestOption<TKey>(this CommandProcessorBuilder<TKey> builder,
            InitOptionsWithUnitTestOption options,
            DateTime? failOverUntil,
            TimeSpan timeMargin,
            Func<string, TKey> deserializeKey,
            IUnitTestGeneratorConfig unitTestGeneratorConfig)
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
            if (options.UnitTest)
                builder = builder.UseApiProxyFactory(new UnitTestGeneratorFactory(unitTestGeneratorConfig,
                    JsonSerializer.CreateDefault(new JsonSerializerSettings().ConfigureForCrabImports())//TODO: optimize the builder so we can get the serializer from the builder instead of assuming it will be this one
                    ));

            builder.SetMinLogLevel(options.LogLevel);

            return builder.SetCommandProcessorOptions(commandProcessorOptions);
        }
    }

    //TODO: I would like a more flexible way of extending commandline options
    // Here I've extended the Init options with a "unit-test" option (I've deliberitely not inherited from Init options as this does not seem to be supported by the Commandline library)
    // This is not something that belongs in "Be.Vlaanderen.Basisregisters.GrAr.Import", so I've created a separate library as an extension.
    // Now, if somebody else wants to something analog, they can, but it is impossible to combine 2 extensions unless they depend on this library but they shouldn't have to
    [Verb("init", HelpText = "Run the event generator in initialize mode")]
    public class InitOptionsWithUnitTestOption
    {
        [Option('c', "clean-start", Default = false, HelpText = "Clean start (reset saved data from previous runs)")]
        public bool CleanStart { get; set; }

        [Option('l', "log-level", Default = LogLevel.Information, HelpText = "Sets the log level (Trace, Debug, Information, Warning, Error, Critical")]
        public LogLevel LogLevel { get; set; }

        [Option('d', "dry-run", Default = false, HelpText = "Process without actually sending anything to the api", SetName = "FakeRun")]
        public bool DryRun { get; set; }

        [Option('u', "unit-test", Default = false, HelpText = "Instead of processing, write a unit test template based on the generated commands per key", SetName = "FakeRun")]
        public bool UnitTest { get; set; }

        [Option('k', "keys", Required = false, Separator = ',', HelpText = "Process a range of keys. Seperated by ,")]
        public IEnumerable<string> Keys { get; set; }

        [Option('t', "take", Required = false, HelpText = "Process one the first x keys, will be ignored if a range of keys is given")]
        public int? Take { get; set; }
    }
}
