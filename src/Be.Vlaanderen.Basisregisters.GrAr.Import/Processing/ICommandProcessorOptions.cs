namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System.Collections.Generic;
    using NodaTime;

    public interface ICommandProcessorOptions<out TKey>
    {
        Instant From { get; }
        Instant Until { get; }
        int? Take { get; }
        IEnumerable<TKey> Keys { get; }
        bool CleanStart { get; }
        ImportMode Mode { get; }
    }
}
