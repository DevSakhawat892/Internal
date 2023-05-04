using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUSO.Infrastructure.SqlServer.Migrations
{
    public partial class ModifiedRecoveryRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecoveryRequests_UserId",
                table: "RecoveryRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryRequests_UserAccounts_UserId",
                table: "RecoveryRequests",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "OID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryRequests_UserId",
                table: "RecoveryRequests",
                column: "UserAccountOID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryRequests_UserAccounts_UserId",
                table: "RecoveryRequests",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "OID");
        }
    }
}
