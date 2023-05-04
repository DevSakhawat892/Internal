using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUSO.Infrastructure.SqlServer.Migrations
{
    public partial class ModifiedHoliday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Previews");

            migrationBuilder.DropColumn(
                name: "SLAID",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "OffDate",
                table: "HolidayLists");

            migrationBuilder.AddColumn<int>(
                name: "HolidayID",
                table: "SLAs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Holidays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Holidays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DayName",
                table: "HolidayLists",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discription",
                table: "HolidayLists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Holiday",
                table: "HolidayLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SLAs_HolidayID",
                table: "SLAs",
                column: "HolidayID");

            migrationBuilder.AddForeignKey(
                name: "FK_SLAs_Holidays_HolidayID",
                table: "SLAs",
                column: "HolidayID",
                principalTable: "Holidays",
                principalColumn: "OID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SLAs_Holidays_HolidayID",
                table: "SLAs");

            migrationBuilder.DropIndex(
                name: "IX_SLAs_HolidayID",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "HolidayID",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "DayName",
                table: "HolidayLists");

            migrationBuilder.DropColumn(
                name: "Discription",
                table: "HolidayLists");

            migrationBuilder.DropColumn(
                name: "Holiday",
                table: "HolidayLists");

            migrationBuilder.AddColumn<int>(
                name: "SLAID",
                table: "Holidays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OffDate",
                table: "HolidayLists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Previews",
                columns: table => new
                {
                    PreviewID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    IncidentTypeID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    UserAccountID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Previews", x => x.PreviewID);
                });
        }
    }
}
