namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;

    public interface ICommandProcessorBatchConfiguration<TKey>
    {
        TimeSpan TimeMargin { get; }
        TKey Deserialize(string key);
    }
}
