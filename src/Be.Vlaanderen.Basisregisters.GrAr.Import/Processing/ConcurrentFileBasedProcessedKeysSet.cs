namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ConcurrentFileBasedProcessedKeysSet<TKey> : IProcessedKeysSet<TKey>
    {
        private readonly Func<string, TKey> _deserializeKey;
        private readonly object _lockObj = new object();
        private readonly string _path;
        private readonly Func<TKey, string> _serializeKey;
        private HashSet<TKey> _cache;

        public ConcurrentFileBasedProcessedKeysSet(Func<TKey, string> serializeKey,
            Func<string, TKey> deserializeKey,
            string path = "ProcessedKeys.log")
        {
            _serializeKey = serializeKey;
            _deserializeKey = deserializeKey;
            _path = path;
        }

        public bool Contains(TKey key)
        {
            if (_cache == null)
                _cache = new HashSet<TKey>(GetAllKeys());

            return _cache.Contains(key);
        }

        public void Add(IEnumerable<TKey> keys)
        {
            lock (_lockObj)
            {
                File.AppendAllLines(_path, keys.Select(_serializeKey));
            }
        }

        public void Clear()
        {
            lock (_lockObj)
            {
                File.Delete(_path);
                if (_cache != null)
                {
                    _cache.Clear();
                    _cache = null;
                }
            }
        }

        private IEnumerable<TKey> GetAllKeys()
        {
            lock (_lockObj)
            {
                return File.Exists(_path)
                    ? File.ReadAllLines(_path).Select(_deserializeKey)
                    : new TKey[] { };
            }
        }
    }
}
