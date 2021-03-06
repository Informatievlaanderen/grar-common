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
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.ImportBatchStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("CrabTimeScope")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ImportFeedId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("Until")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("From", "ImportFeedId")
                        .IsUnique();

                    //b.ToTable("ImportStatus","dbo");
                    b.ToTable(Schema.StatusTable, Schema.Name);
                });
#pragma warning restore 612, 618
        }
    }
}
