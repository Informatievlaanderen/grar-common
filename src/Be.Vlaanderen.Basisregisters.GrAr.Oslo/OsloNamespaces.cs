namespace Be.Vlaanderen.Basisregisters.GrAr.Oslo;

public static class OsloNamespaces
{
    public const string OsloNamespacePrefix = "https://data.vlaanderen.be/id";

    public static readonly OsloNamespace Gemeente = new OsloNamespace(OsloNamespacePrefix + "/gemeente");
    public static readonly OsloNamespace GemeenteStatus = new OsloNamespace(OsloNamespacePrefix + "/concept/gemeentestatus");
    public static readonly OsloNamespace Postinfo = new OsloNamespace($"{OsloNamespacePrefix}/postinfo");
    public static readonly OsloNamespace PostinfoStatus = new OsloNamespace(OsloNamespacePrefix + "/concept/postinfostatus");
    public static readonly OsloNamespace StraatNaam = new OsloNamespace($"{OsloNamespacePrefix}/straatnaam");
    public static readonly OsloNamespace StraatNaamStatus = new OsloNamespace(OsloNamespacePrefix + "/concept/straatnaamstatus");
    public static readonly OsloNamespace Adres = new OsloNamespace($"{OsloNamespacePrefix}/adres");
    public static readonly OsloNamespace AdresStatus = new OsloNamespace(OsloNamespacePrefix + "/concept/adresstatus");
    public static readonly OsloNamespace AdresGeometrieMethode = new OsloNamespace(OsloNamespacePrefix + "/concept/geometriemethode");
    public static readonly OsloNamespace AdresGeometrieSpecificatie = new OsloNamespace(OsloNamespacePrefix + "/concept/geometriespecificatie");
    public static readonly OsloNamespace Gebouw = new OsloNamespace($"{OsloNamespacePrefix}/gebouw");
    public static readonly OsloNamespace Gebouweenheid = new OsloNamespace($"{OsloNamespacePrefix}/gebouweenheid");
    public static readonly OsloNamespace Perceel = new OsloNamespace($"{OsloNamespacePrefix}/perceel");
    public static readonly OsloNamespace Wegknoop = new OsloNamespace($"{OsloNamespacePrefix}/wegknoop");
    public static readonly OsloNamespace Wegsegment = new OsloNamespace($"{OsloNamespacePrefix}/wegsegment");
    public static readonly OsloNamespace GelijkgrondseKruising = new OsloNamespace($"{OsloNamespacePrefix}/gelijkgrondsekruising");
    public static readonly OsloNamespace OngelijkgrondseKruising = new OsloNamespace($"{OsloNamespacePrefix}/ongelijkgrondsekruising");
}
