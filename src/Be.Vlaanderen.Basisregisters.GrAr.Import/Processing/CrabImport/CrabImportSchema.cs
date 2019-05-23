namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;

    public class CrabImportSchema
    {
        public string Name { get; }
        public string StatusTable { get; }

        public CrabImportSchema(
            string name,
            string statusTableName)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
            StatusTable = string.IsNullOrWhiteSpace(statusTableName) ? throw new ArgumentNullException(nameof(statusTableName)) : statusTableName;
        }

        public static class Default
        {
            public const string StatusName = "ImportStatus";
        }
    }
}
