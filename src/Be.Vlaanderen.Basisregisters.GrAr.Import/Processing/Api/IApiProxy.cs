namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System.Collections.Generic;

    public interface IApiProxy
    {
        void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports);

        ICommandProcessorOptions<TKey> InitializeImport<TKey>(
            ImportOptions options,
            ICommandProcessorBatchConfiguration<TKey> configuration);

        void FinalizeImport<TKey>(ICommandProcessorOptions<TKey> options);
    }
}
