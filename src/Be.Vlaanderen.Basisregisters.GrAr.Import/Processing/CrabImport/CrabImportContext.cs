namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport
{
    using System;
    using System.Linq;
    using Api.Messages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Newtonsoft.Json;

    public class CrabImportContext : DbContext
    {
        private readonly CrabImportSchema _crabImportSchema;
        public DbSet<ImportBatchStatus> BatchStatuses { get; set; }

        public BatchStatus LastBatchFor(ImportFeed feed)
        {
            var lastStatus = BatchStatuses
                ?.Where(status => status.ImportFeedId == feed.Name)
                .OrderBy(status => status.From)
                .LastOrDefault();

            return lastStatus == null
                ? null
                : new BatchStatus
                {
                    From = lastStatus.From,
                    Until = lastStatus.Until,
                    Completed = lastStatus.Completed
                };
        }

        public void SetCurrent(BatchStatusUpdate status)
        {
            if (status == null)
                return;

            if (status.Until == DateTime.MinValue || string.IsNullOrWhiteSpace(status.ImportFeed?.Name))
                throw new ArgumentException($"Invalid batch status : {JsonConvert.SerializeObject(status)}");

            var currentStatus = BatchStatuses.Find(status.From, status.ImportFeed.Name);
            if (currentStatus == null)
            {
                BatchStatuses.Add(
                    new ImportBatchStatus
                    {
                        ImportFeedId = status.ImportFeed.Name,
                        From = status.From,
                        Until = status.Until,
                        Completed = status.Completed
                    });
            }
            else
            {
                currentStatus.Completed = status.Completed;
            }
        }

        public CrabImportContext(
            DbContextOptions<CrabImportContext> options,
            CrabImportSchema schema)
            : base(options) =>
            _crabImportSchema = schema ?? throw new ArgumentNullException(nameof(schema));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var batchStatus = modelBuilder.Entity<ImportBatchStatus>();
            batchStatus
                .ToTable(_crabImportSchema.StatusTable, _crabImportSchema.Name)
                .HasKey(status => new { status.From, ImportFeed = status.ImportFeedId });

            batchStatus.Property(status => status.ImportFeedId);
            batchStatus.Property(status => status.From);
            batchStatus.Property(status => status.Until);
            batchStatus.Property(status => status.Completed);
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
