namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;

    public interface ICommandProcessorOptions<TKey>
    {
        DateTime From { get; }
        DateTime Until { get; }
        int? Take { get; }
        IEnumerable<TKey> Keys { get; }
        bool CleanStart { get; }
        ImportMode Mode { get; }
    }
}
