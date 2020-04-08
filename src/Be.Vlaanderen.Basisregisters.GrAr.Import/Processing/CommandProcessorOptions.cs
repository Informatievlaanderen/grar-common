namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Crab;
    using NodaTime;

    public class CommandProcessorOptions<TKey> : ICommandProcessorOptions<TKey>
    {
        public Instant From { get; set; }
        public Instant Until { get; set; }
        public int? Take { get; set; }
        public IEnumerable<TKey> Keys { get; }
        public bool CleanStart { get; }
        public ImportMode Mode { get; }

        public CommandProcessorOptions(
            Instant from,
            Instant until,
            IEnumerable<TKey> keys,
            int? take,
            bool cleanStart,
            ImportMode mode)
        {
            From = from;
            Until = until;
            Mode = mode;
            CleanStart = cleanStart;
            Keys = keys;
            Take = take;
        }

        public override string ToString() =>
            $"{Environment.NewLine}" +
            $"From: {From.ToDateTimeOffset()}{Environment.NewLine}" +
            $"Until: {Until.ToDateTimeOffset()}{Environment.NewLine}" +
            $"CrabTimeScope: {From.ToCrabDateTime()} - {Until.ToCrabDateTime()}{Environment.NewLine}" +
            (Keys != null && Keys.Any() ? $"Keys: {string.Join(", ", Keys)}{Environment.NewLine}" : "") +
            (Take.HasValue ? $"Take: {Take.Value}{Environment.NewLine}" : "") +
            $"CleanStart: {CleanStart}{Environment.NewLine}" +
            $"Mode: {Enum.GetName(typeof(ImportMode), Mode)}";
    }
}
