namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public static class CrabImportMigrationsHelper
    {
        internal static CrabImportSchema CrabImportSchema { get; private set; }

        public static void Run(
            string connectionString,
            string migrationsSchema,
            string migrationsTableName,
            CrabImportSchema crabImportSchema,
            ILoggerFactory loggerFactory = null)
        {
            var migrationOptionsBuilder = new DbContextOptionsBuilder<CrabImportContext>()
                .UseSqlServer(
                    connectionString,
                    x => x.MigrationsHistoryTable(migrationsTableName, migrationsSchema));

            if (loggerFactory != null)
                migrationOptionsBuilder = migrationOptionsBuilder.UseLoggerFactory(loggerFactory);

            using (var migrator = new CrabImportContext(migrationOptionsBuilder.Options, crabImportSchema))
            {
                // Static reference is used to set the schema and tables in the migrations
                CrabImportSchema = crabImportSchema;
                migrator.Database.Migrate();
            }
        }
    }
}
