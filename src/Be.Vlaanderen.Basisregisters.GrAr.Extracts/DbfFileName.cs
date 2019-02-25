namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Api.Extract;

    public class DbfFileName : ExtractFileName
    {
        public DbfFileName(string name)
            : base(name, "dbf") { }
    }
}
