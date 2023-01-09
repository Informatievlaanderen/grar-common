namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class CrabImportModule
    {
        public CrabImportModule(
            IServiceCollection services,
            string connectionString,
            string schema,
            ILoggerFactory loggerFactory)
            : this(
                services,
                connectionString,
                schema,
                CrabImportSchema.Default.StatusName,
                MigrationsSchema.Default.HistoryTable,
                loggerFactory)
        { }

        public CrabImportModule(
            IServiceCollection services,
            string connectionString,
            string schema,
            string statusTableName,
            string migrationsHistoryTableName,
            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<CrabImportContext>();
            var configuration = new Configuration(connectionString);
            var migrationsSchema = new MigrationsSchema(schema, migrationsHistoryTableName);
            var importSchema = new CrabImportSchema(schema, statusTableName);

            services.AddSingleton(configuration);
            services.AddSingleton(migrationsSchema);
            services.AddSingleton(importSchema);

            services.AddDbContext<CrabImportContext>(
                options => options
                    .UseLoggerFactory(loggerFactory)
                    .UseSqlServer(
                        configuration.ConnectionString,
                        x => x.MigrationsHistoryTable(migrationsSchema.HistoryTable, migrationsSchema.Name)));

            logger.LogInformation(
                "Added {Context} to services:" +
                Environment.NewLine +
                "\tSchema: {Schema}" +
                Environment.NewLine +
                "\tTableName: {TableName}",
                nameof(CrabImportContext), migrationsSchema.Name, migrationsSchema.HistoryTable);
        }
    }

    public static class CrabImportExtensions
    {
        public static IServiceCollection ConfigureCrabImport(
            this IServiceCollection services,
            string connectionString,
            string schema,
            string statusTableName,
            string migrationsHistoryTableName,
            ILoggerFactory loggerFactory)
        {
            var _ = new CrabImportModule(services, connectionString, schema, statusTableName, migrationsHistoryTableName, loggerFactory);

            return services;
        }

        public static IServiceCollection ConfigureCrabImport(
            this IServiceCollection services,
            string connectionString,
            string schema,
            ILoggerFactory loggerFactory)
        {
            var _ = new CrabImportModule(services, connectionString, schema, CrabImportSchema.Default.StatusName, MigrationsSchema.Default.HistoryTable, loggerFactory);

            return services;
        }
    }
}
