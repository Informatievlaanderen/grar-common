namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    public interface IHaveHash : IHaveHashFields
    {
        public string GetHash();
    }
}
