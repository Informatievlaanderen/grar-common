namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System.Collections.Generic;

    public interface IProcessedKeysSet<TKey>
    {
        bool Contains(TKey key);
        void Add(IEnumerable<TKey> keys);
        void Clear();
    }
}
