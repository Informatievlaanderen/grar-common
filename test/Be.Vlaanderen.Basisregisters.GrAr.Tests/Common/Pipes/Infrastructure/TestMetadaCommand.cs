namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Common.Pipes.Infrastructure
{
    public class TestMetadataCommand
    {
        public string Name { get; set; }

        public TestMetadataCommand(string name)
        {
            Name = name;
        }
    }
}
