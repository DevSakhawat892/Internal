using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUSO.Infrastructure.SqlServer.Migrations
{
    public partial class CreateTierLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDepertmentHead",
                table: "Designations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RouteTypes",
                columns: table => new
                {
                    OID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTypes", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "TierLevels",
                columns: table => new
                {
                    OID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    IncidentTypeID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TierLevels", x => x.OID);
                    table.ForeignKey(
                        name: "FK_TierLevels_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TierLevels_IncidentTypes_IncidentTypeID",
                        column: x => x.IncidentTypeID,
                        principalTable: "IncidentTypes",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketRoutings",
                columns: table => new
                {
                    OID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallCenterID = table.Column<long>(type: "bigint", nullable: true),
                    CallCenterRefferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    DepartmentRefferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignTo = table.Column<long>(type: "bigint", nullable: true),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RouteTypeID = table.Column<int>(type: "int", nullable: false),
                    IncidentID = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRoutings", x => x.OID);
                    table.ForeignKey(
                        name: "FK_TicketRoutings_Incidents_IncidentID",
                        column: x => x.IncidentID,
                        principalTable: "Incidents",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRoutings_RouteTypes_RouteTypeID",
                        column: x => x.RouteTypeID,
                        principalTable: "RouteTypes",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketRoutings_IncidentID",
                table: "TicketRoutings",
                column: "IncidentID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRoutings_RouteTypeID",
                table: "TicketRoutings",
                column: "RouteTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TierLevels_DepartmentID",
                table: "TierLevels",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_TierLevels_IncidentTypeID",
                table: "TierLevels",
                column: "IncidentTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketRoutings");

            migrationBuilder.DropTable(
                name: "TierLevels");

            migrationBuilder.DropTable(
                name: "RouteTypes");

            migrationBuilder.DropColumn(
                name: "IsDepertmentHead",
                table: "Designations");
        }
    }
}
