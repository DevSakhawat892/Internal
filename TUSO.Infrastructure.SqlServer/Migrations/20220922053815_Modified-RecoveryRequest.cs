using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUSO.Infrastructure.SqlServer.Migrations
{
    public partial class ModifiedRecoveryRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "RecoveryRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryRequests_UserAccounts_UserAccountOID",
                table: "RecoveryRequests");

            migrationBuilder.DropIndex(
                name: "IX_RecoveryRequests_UserAccountOID",
                table: "RecoveryRequests");

            migrationBuilder.DropColumn(
                name: "UserAccountOID",
                table: "RecoveryRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RecoveryRequests");
        }
    }
}
