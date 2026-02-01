namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo;

public static class OsloNamespaces
{
    public const string OsloNamespacePrefix = "https://data.vlaanderen.be/id";

    public static readonly OsloNamespace Gemeente = new OsloNamespace(OsloNamespacePrefix + "/gemeente");
    public static readonly OsloNamespace Postinfo = $"{OsloNamespacePrefix}/postinfo";
    public static readonly OsloNamespace StraatNaam = $"{OsloNamespacePrefix}/straatnaam";
    public static readonly OsloNamespace Adres = $"{OsloNamespacePrefix}/adres";
    public static readonly OsloNamespace Gebouw = $"{OsloNamespacePrefix}/gebouw";
    public static readonly OsloNamespace Gebouweenheid = $"{OsloNamespacePrefix}/gebouweenheid";
    public static readonly OsloNamespace Perceel = $"{OsloNamespacePrefix}/perceel";
    public static readonly OsloNamespace Wegknoop = $"{OsloNamespacePrefix}/wegknoop";
    public static readonly OsloNamespace Wegsegment = $"{OsloNamespacePrefix}/wegsegment";
}
