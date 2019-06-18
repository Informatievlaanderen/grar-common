// <auto-generated />
using System;
using Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CrabImport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Migrations
{
    [DbContext(typeof(CrabImportContext))]
    partial class CrabImportContextModelSnapshot : ModelSnapshot
    {
        private static CrabImportSchema Schema => CrabImportMigrationsHelper.CrabImportSchema;

        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.ImportBatchStatus", b =>
                {
                    b.Property<DateTime>("From");

                    b.Property<string>("ImportFeedId");

                    b.Property<bool>("Completed");

                    b.Property<DateTime>("Until");

                    b.HasKey("From", "ImportFeedId");

                    b.ToTable(Schema.StatusTable, Schema.Name);
                });
#pragma warning restore 612, 618
        }
    }
}
