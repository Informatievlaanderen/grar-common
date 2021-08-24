namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Api.Extract;

    public class MetadataDbfFileName : ExtractFileName
    {
        public MetadataDbfFileName(string name)
            : base($"{name}_metadata", "dbf") { }
    }
}
