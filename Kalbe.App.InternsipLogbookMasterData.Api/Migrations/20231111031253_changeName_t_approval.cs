using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class changeName_t_approval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_Approval)_m_Approval_ApprovalId",
                table: "t_Approval)");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_Approval)",
                table: "t_Approval)");

            migrationBuilder.RenameTable(
                name: "t_Approval)",
                newName: "t_Approval");

            migrationBuilder.RenameIndex(
                name: "IX_t_Approval)_ApprovalId",
                table: "t_Approval",
                newName: "IX_t_Approval_ApprovalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_Approval",
                table: "t_Approval",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_Approval_m_Approval_ApprovalId",
                table: "t_Approval",
                column: "ApprovalId",
                principalTable: "m_Approval",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_Approval_m_Approval_ApprovalId",
                table: "t_Approval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_Approval",
                table: "t_Approval");

            migrationBuilder.RenameTable(
                name: "t_Approval",
                newName: "t_Approval)");

            migrationBuilder.RenameIndex(
                name: "IX_t_Approval_ApprovalId",
                table: "t_Approval)",
                newName: "IX_t_Approval)_ApprovalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_Approval)",
                table: "t_Approval)",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_Approval)_m_Approval_ApprovalId",
                table: "t_Approval)",
                column: "ApprovalId",
                principalTable: "m_Approval",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
