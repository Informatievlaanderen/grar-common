namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using CommandHandling.Idempotency;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public static class CrabImportMigrationsHelper
    {
        internal static CrabImportSchema CrabImportSchema { get; private set; }

        internal static void Run(
            Configuration configuration,
            MigrationsSchema migrationsSchema,
            CrabImportSchema crabImportSchema,
            ILoggerFactory loggerFactory)
        {
            var migrationOptionsBuilder = new DbContextOptionsBuilder<CrabImportContext>()
                .UseSqlServer(
                    configuration.ConnectionString,
                    x => x.MigrationsHistoryTable(migrationsSchema.HistoryTable, migrationsSchema.Name));

            if (loggerFactory != null)
                migrationOptionsBuilder = migrationOptionsBuilder.UseLoggerFactory(loggerFactory);

            using var migrator = new CrabImportContext(migrationOptionsBuilder.Options, crabImportSchema);

            // Static reference is used to set the schema and tables in the migrations
            CrabImportSchema = crabImportSchema;
            migrator.Database.Migrate();
        }
    }
}
