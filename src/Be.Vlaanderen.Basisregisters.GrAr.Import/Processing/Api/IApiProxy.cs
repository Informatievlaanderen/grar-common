namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Api
{
    using System.Collections.Generic;

    public interface IApiProxy
    {
        void ImportBatch<TKey>(IEnumerable<KeyImport<TKey>> imports);
    }
}
