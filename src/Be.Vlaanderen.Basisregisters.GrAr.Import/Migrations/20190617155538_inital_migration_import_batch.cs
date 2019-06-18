using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Migrations
{
    using Processing.CrabImport;

    public partial class inital_migration_import_batch : Migration
    {
        private static CrabImportSchema Schema => CrabImportMigrationsHelper.CrabImportSchema;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: Schema.Name);

            migrationBuilder.CreateTable(
                name: Schema.StatusTable,
                schema: Schema.Name,
                columns: table => new
                {
                    ImportFeedId = table.Column<string>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStatus", x => new { x.From, x.ImportFeedId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: Schema.StatusTable,
                schema: Schema.Name);
        }
    }
}
