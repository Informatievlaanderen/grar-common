namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System.Collections.Generic;

    public interface IApiProxy
    {
        void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports);

        ICommandProcessorOptions<TKey> GetImportOptions<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration);

        void InitializeImport<TKey>(ICommandProcessorOptions<TKey> options);

        void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options);
    }
}
