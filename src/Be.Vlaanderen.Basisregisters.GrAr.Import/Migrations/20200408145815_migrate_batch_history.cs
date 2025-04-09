using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Migrations
{
    using Processing.CrabImport;

    public partial class migrate_batch_history : Migration
    {
        private static CrabImportSchema? Schema => CrabImportMigrationsHelper.CrabImportSchema;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if(Schema == null)
                throw new InvalidOperationException("Schema is not set.");

            migrationBuilder.EnsureSchema(name: Schema.Name);

            //CREATE TEMP TABLE + FILL
            const string tempImportStatusTableName = "tempImportStatus";
            migrationBuilder.CreateTable(
                tempImportStatusTableName,
                schema: Schema.Name,
                columns: table => new
                {
                    ImportFeedId = table.Column<string>(nullable: false, type: "nvarchar(450)"),
                    From = table.Column<DateTime>(nullable: false),
                    FromUtc = table.Column<DateTimeOffset>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false),
                    UntilUtc = table.Column<DateTimeOffset>(nullable: false),
                });

            migrationBuilder.Sql($@"
INSERT INTO {Schema.Name}.{tempImportStatusTableName} (ImportFeedId, [From], FromUtc, Until, UntilUtc)
SELECT
    ImportFeedId,
    [from],
    TODATETIMEOFFSET(
        CASE
		    WHEN [from] <= '0001-01-01 02:00:00' THEN [from] AT TIME ZONE 'UTC'
		    ELSE ([from] at time zone 'Central Europe Standard Time') AT TIME ZONE 'UTC'
	    END,
    '+00:00'),
    [until],
    TODATETIMEOFFSET(([until] at time zone 'Central Europe Standard Time') AT TIME ZONE 'UTC', '+00:00')
FROM {Schema.Name}.{Schema.StatusTable}
WHERE Completed = 1"); //Needed to be sure to only collapse completed batches, potential data loss if one is not completed!!

            //CLEAR CURRENT TABLE
            migrationBuilder.Sql($"TRUNCATE TABLE {Schema.Name}.{Schema.StatusTable}");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImportStatus",
                schema: Schema.Name,
                table:Schema.StatusTable);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Until",
                schema: Schema.Name,
                table:Schema.StatusTable,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "From",
                schema: Schema.Name,
                table:Schema.StatusTable,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: Schema.Name,
                table:Schema.StatusTable,
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CrabTimeScope",
                schema: Schema.Name,
                table:Schema.StatusTable,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImportStatus",
                schema: Schema.Name,
                table:Schema.StatusTable,
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImportStatus_From_ImportFeedId",
                schema: Schema.Name,
                table:Schema.StatusTable,
                columns: new[] { "From", "ImportFeedId" },
                unique: true);

            //FILL CURRENT TABLE
            migrationBuilder.Sql(@$"
INSERT INTO {Schema.Name}.{Schema.StatusTable} (ImportFeedId, [From], Until, CrabTimeScope, Completed)
SELECT ImportFeedId,
       MIN(FromUtc),
       MAX(UntilUtc),
       CONCAT(MIN([From]), ' - ', MAX(Until)),
       1
FROM {Schema.Name}.{tempImportStatusTableName}
GROUP BY ImportFeedId");

            //DELETE TEMP TABLE
            migrationBuilder.DropTable(
                name: tempImportStatusTableName,
                schema: Schema.Name);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ImportStatus",
                schema: Schema!.Name,
                table:Schema.StatusTable);

            migrationBuilder.DropIndex(
                name: "IX_ImportStatus_From_ImportFeedId",
                schema: Schema.Name,
                table:Schema.StatusTable);

            migrationBuilder.DropColumn(
                name: "Id",
                schema: Schema.Name,
                table:Schema.StatusTable);

            migrationBuilder.DropColumn(
                name: "CrabTimeScope",
                schema: Schema.Name,
                table:Schema.StatusTable);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Until",
                schema: Schema.Name,
                table:Schema.StatusTable,
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "From",
                schema: Schema.Name,
                table:Schema.StatusTable,
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImportStatus",
                schema: Schema.Name,
                table:Schema.StatusTable,
                columns: new[] { "From", "ImportFeedId" });
        }
    }
}
