namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class CrabImportContext : DbContext
    {
        private readonly CrabImportSchema _crabImportSchema;
        public DbSet<ImportBatchStatus> BatchStatuses { get; set; }

        public ImportBatchStatus LastBatch =>
            BatchStatuses
                ?.OrderBy(status => status.From)
                .LastOrDefault();

        public void SetCurrent(ImportBatchStatus batchStatus)
        {
            if (batchStatus == null)
                return;

            if (batchStatus.IsInvalid)
                throw new ArgumentException($"Invalid batch status : {batchStatus}");

            var currentStatus = BatchStatuses.Find(batchStatus.From);
            if (currentStatus == null)
            {
                BatchStatuses.Add(batchStatus);
            }
            else
            {
                currentStatus.Completed = batchStatus.Completed;
            }
        }

        public CrabImportContext(
            DbContextOptions<CrabImportContext> options,
            CrabImportSchema schema)
            : base(options)
        {
            _crabImportSchema = schema ?? throw new ArgumentNullException(nameof(schema));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var batchStatus = modelBuilder.Entity<ImportBatchStatus>();

            batchStatus
                .ToTable(_crabImportSchema.StatusTable, _crabImportSchema.Name)
                .HasKey(status => status.From);

            batchStatus.Property(status => status.From);
            batchStatus.Property(status => status.Until);
            batchStatus.Property(status => status.Completed);
        }
    }

    public class CrabImportSchema
    {
        public string Name { get; }
        public string StatusTable { get; }

        public CrabImportSchema(
            string schemaName,
            string statusTableName)
        {
            Name = string.IsNullOrWhiteSpace(schemaName) ? throw new ArgumentNullException(nameof(schemaName)) : schemaName;
            StatusTable = string.IsNullOrWhiteSpace(statusTableName) ? throw new ArgumentNullException(nameof(statusTableName)) : statusTableName;
        }

        public static class Default
        {
            public const string StatusName = "ImportStatus";
        }
    }

    public class CrabImportContextFactory : IDesignTimeDbContextFactory<CrabImportContext>
    {
        public CrabImportContext CreateDbContext(string[] args)
        {
            var schema = new CrabImportSchema("dbo", CrabImportSchema.Default.StatusName);
            var options = new DbContextOptionsBuilder<CrabImportContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory.CrabImportContextContext;Trusted_Connection=True;")
                .Options;

            return new CrabImportContext(options, schema);
        }
    }
}
