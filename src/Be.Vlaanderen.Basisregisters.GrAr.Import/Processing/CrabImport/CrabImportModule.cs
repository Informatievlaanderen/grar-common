namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;
    using Autofac;
    using DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class CrabImportModule : Module, IServiceCollectionModule
    {
        private readonly string _connectionString;
        private readonly string _schema;
        private readonly string _statusTableName;
        private readonly string _migrationsHistoryTableName;
        private readonly ILoggerFactory _loggerFactory;

        public CrabImportModule(
            string connectionString,
            string schema,
            ILoggerFactory loggerFactory)
            : this(
                connectionString,
                schema,
                CrabImportSchema.Default.StatusName,
                MigrationsSchema.Default.HistoryTable,
                loggerFactory)
        { }

        public CrabImportModule(
            string connectionString,
            string schema,
            string statusTableName,
            string migrationsHistoryTableName,
            ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
            _schema = schema;
            _statusTableName = statusTableName;
            _migrationsHistoryTableName = migrationsHistoryTableName;
            _loggerFactory = loggerFactory;
        }

        public void Load(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<CrabImportContext>();
            var configuration = new Configuration(_connectionString);
            var migrationsSchema = new MigrationsSchema(_schema, _migrationsHistoryTableName);
            var importSchema = new CrabImportSchema(_schema, _statusTableName);

            services.AddSingleton(configuration);
            services.AddSingleton(migrationsSchema);
            services.AddSingleton(importSchema);

            services.AddDbContext<CrabImportContext>(
                options => options
                    .UseLoggerFactory(_loggerFactory)
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
}
