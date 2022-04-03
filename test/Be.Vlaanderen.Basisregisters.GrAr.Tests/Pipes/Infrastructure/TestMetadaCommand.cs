namespace Be.Vlaanderen.Basisregisters.GrAr.Tests.Pipes.Infrastructure
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
