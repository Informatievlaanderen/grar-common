namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommandProcessorOptions<TKey> : ICommandProcessorOptions<TKey>
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public int? Take { get; set; }
        public IEnumerable<TKey> Keys { get; }
        public bool CleanStart { get; }
        public ImportMode Mode { get; }

        public CommandProcessorOptions(
            DateTime from,
            DateTime until,
            IEnumerable<TKey> keys = null,
            int? take = null,
            bool cleanStart = false,
            ImportMode mode = ImportMode.Init)
        {
            From = from;
            Until = until;
            Mode = mode;
            CleanStart = cleanStart;
            Keys = keys;
            Take = take;
        }

        public override string ToString() => $"{Environment.NewLine}" +
                                             $"From: {From}{Environment.NewLine}" +
                                             $"Until: {Until}{Environment.NewLine}" +
                                             (Keys != null && Keys.Any() ? $"Keys: {string.Join(", ", Keys)}{Environment.NewLine}" : "") +
                                             (Take.HasValue ? $"Take: {Take.Value}{Environment.NewLine}" : "") +
                                             $"CleanStart: {CleanStart}{Environment.NewLine}" +
                                             $"Mode: {Enum.GetName(typeof(ImportMode), Mode)}"; //{Environment.NewLine}";
    }
}
