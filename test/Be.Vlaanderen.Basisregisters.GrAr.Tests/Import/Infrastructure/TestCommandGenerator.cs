namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Import.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using GrAr.Import.Processing.Generate;

    public class TestCommandGenerator : ICommandGenerator<int>
    {
        private readonly int _averageDuration;
        private readonly int _nrOfCommandsPerKey;
        private readonly int _nrOfKeys;

        public string Name => "TestCommandGenerator";

        public TestCommandGenerator(int nrOfKeys = 20,
            int nrOfCommandsPerKey = 20,
            int averageDuration = 5)
        {
            _nrOfCommandsPerKey = nrOfCommandsPerKey;
            _averageDuration = averageDuration;
            _nrOfKeys = nrOfKeys;
        }

        public IEnumerable<int> GetChangedKeys(DateTime from,
            DateTime until) => Enumerable.Range(0, _nrOfKeys);

        public IEnumerable<dynamic> GenerateInitCommandsFor(int key,
            DateTime from,
            DateTime until)
        {
            Thread.Sleep(_averageDuration);
            return Enumerable.Range(0, _nrOfCommandsPerKey).Select(i => new { });
        }

        public IEnumerable<dynamic> GenerateUpdateCommandsFor(int key,
            DateTime from,
            DateTime until)
        {
            Thread.Sleep(_averageDuration);
            return Enumerable.Range(0, _nrOfCommandsPerKey).Select(i => new { });
        }
    }
}
