namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Xunit
{
    public interface IUnitTestGeneratorConfig
    {
        string BasePath { get; }


        string NamespaceName { get; }
        string ClassNamePrefix { get; }
        string BaseClassName { get; }

        string GetClassName(object key);
        string GetCreateIdStatement(object key);
    }
}