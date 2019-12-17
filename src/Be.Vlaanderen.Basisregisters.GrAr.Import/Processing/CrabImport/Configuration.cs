namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;

    internal class Configuration
    {
        public string ConnectionString { get; }

        public Configuration(string connectionString)
        {
            ConnectionString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentNullException(connectionString)
                : connectionString;
        }
    }
}
