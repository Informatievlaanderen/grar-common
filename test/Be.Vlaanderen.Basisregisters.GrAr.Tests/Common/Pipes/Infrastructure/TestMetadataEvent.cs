namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    using System.Collections.Generic;
    using EventHandling;
    using GrAr.Common;

    [EventName(EventName)]
    public class TestMetadataEvent : IHaveHash
    {
        private const string EventName = "TestMetadataEventHash";
        public string Name { get; set; }

        public TestMetadataEvent(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetHashFields()
        {
            yield return Name;
        }

        public string GetHash() => this.ToEventHash(EventName);
    }
}
