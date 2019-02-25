namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using Api.Extract;

    public class ShpFileName : ExtractFileName
    {
        public ShpFileName(string name)
            : base(name, "shp") { }
    }
}
