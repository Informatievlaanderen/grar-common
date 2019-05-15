namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System.Collections.Generic;

    public interface IApiProxy
    {
        void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports);

        ICommandProcessorOptions<TKey> InitialiseImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration);

        void FinaliseImport<TKey>(ICommandProcessorOptions<TKey> options);
    }
}
