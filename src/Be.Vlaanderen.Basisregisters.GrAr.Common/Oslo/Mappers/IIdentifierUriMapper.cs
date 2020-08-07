namespace Be.Vlaanderen.Basisregisters.GrAr.Common.Oslo.Mappers
{
    public interface IIdentifierUriMapper<out T>
    {
        T Map(IdentifierUri identifier);
    }
}
