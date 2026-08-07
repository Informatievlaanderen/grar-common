namespace Be.Vlaanderen.Basisregisters.GrAr.Common.NetTopology;

public static class CoordinateSystemWkt
{
    public const string EsriLambert1972 =
        """PROJCS["Belge_Lambert_1972",GEOGCS["GCS_Belge_1972",DATUM["D_Belge_1972",SPHEROID["International_1924",6378388.0,297.0]],PRIMEM["Greenwich",0.0],UNIT["Degree",0.0174532925199433]],PROJECTION["Lambert_Conformal_Conic"],PARAMETER["False_Easting",150000.01256],PARAMETER["False_Northing",5400088.4378],PARAMETER["Central_Meridian",4.367486666666666],PARAMETER["Standard_Parallel_1",49.8333339],PARAMETER["Standard_Parallel_2",51.16666723333333],PARAMETER["Latitude_Of_Origin",90.0],UNIT["Meter",1.0]]""";

    public const string EsriLambert2008 =
        """PROJCS["Belge_Lambert_2008",GEOGCS["GCS_ETRS_1989",DATUM["D_ETRS_1989",SPHEROID["GRS_1980",6378137.0,298.257222101]],PRIMEM["Greenwich",0.0],UNIT["Degree",0.0174532925199433]],PROJECTION["Lambert_Conformal_Conic"],PARAMETER["False_Easting",649328.0],PARAMETER["False_Northing",665262.0],PARAMETER["Central_Meridian",4.359215833333333],PARAMETER["Standard_Parallel_1",49.83333333333334],PARAMETER["Standard_Parallel_2",51.16666666666666],PARAMETER["Latitude_Of_Origin",50.797815],UNIT["Meter",1.0]]""";
}
