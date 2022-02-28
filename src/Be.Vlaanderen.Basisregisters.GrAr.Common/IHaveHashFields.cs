namespace Be.Vlaanderen.Basisregisters.GrAr.Common
{
    using System.Collections.Generic;

    public interface IHaveHashFields
    {
        public IEnumerable<string> GetHashFields();
    }
}
