namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;

    public interface ICommandProcessorBatchConfiguration<out TKey>
    {
        TimeSpan TimeMargin { get; }
        TKey Deserialize(string key);
    }
}
