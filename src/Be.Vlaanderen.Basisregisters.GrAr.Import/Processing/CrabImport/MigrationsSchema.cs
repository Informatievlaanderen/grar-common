namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;

    internal class MigrationsSchema
    {
        public string Name { get; }
        public string HistoryTable { get; }

        public MigrationsSchema(
            string name,
            string historyTable)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
            HistoryTable = string.IsNullOrWhiteSpace(historyTable) ? throw new ArgumentNullException(nameof(historyTable)) : historyTable;
        }

        public static class Default
        {
            public const string HistoryTable = "__EFMigrationsHistoryCrabImport";
        }
    }
}
