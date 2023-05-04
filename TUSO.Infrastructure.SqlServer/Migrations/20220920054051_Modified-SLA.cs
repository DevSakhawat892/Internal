using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUSO.Infrastructure.SqlServer.Migrations
{
    public partial class ModifiedSLA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "SLAs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SLAs",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "SLAs",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SLAs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "SLAs",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SLAs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SLAs");
        }
    }
}
