namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CommandLine
{
    using System;
    using System.Collections.Generic;
    using global::CommandLine;
    using Microsoft.Extensions.Logging;

    public abstract class ImportArguments
    {
        [Option('c', "clean-start", Default = false, HelpText = "Clean start (reset saved data from previous runs)")]
        public bool CleanStart { get; set; }

        [Option('l', "log-level", Default = LogLevel.Information, HelpText = "Sets the log level (Trace, Debug, Information, Warning, Error, Critical")]
        public LogLevel LogLevel { get; set; }

        [Option('d', "dry-run", Default = false, HelpText = "Process without actually sending anything to the api")]
        public bool DryRun { get; set; }

        [Option('k', "keys", Required = false, Separator = ',', HelpText = "Process a range of keys. Seperated by ,")]
        public IEnumerable<string> Keys { get; set; }
    }

    [Verb("init", HelpText = "Run the event generator in initialize mode")]
    public class InitArguments : ImportArguments
    {
        [Option('t', "take", Required = false, HelpText = "Process one the first x keys, will be ignored if a range of keys is given")]
        public int? Take { get; set; }
    }

    [Verb("update", HelpText = "Run the event generator in update mode (default)")]
    public class UpdateArguments : ImportArguments
    {
        [Option("until", Required = false, HelpText = "Looks up all changed ids until this timestamp. If empty uses default behavior.")]
        public DateTime? Until { get; set; }
    }
}
