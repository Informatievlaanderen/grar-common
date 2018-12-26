namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using GrAr.Import.Processing;

    public class TestProcessedKeysSet : IProcessedKeysSet<int>
    {
        public ConcurrentBag<int> Keys { get; }

        public int Count => Keys.Count;

        public TestProcessedKeysSet() => Keys = new ConcurrentBag<int>();

        public bool Contains(int key) => Keys.Contains(key);

        public void Add(IEnumerable<int> keys)
        {
            foreach (var i in keys) Add(i);
        }

        public void Clear()
        {
            Keys.Clear();
        }

        public void Add(int key)
        {
            Keys.Add(key);
        }
    }
}
