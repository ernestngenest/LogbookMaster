using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class edit_constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance");

            migrationBuilder.AlterColumn<long>(
                name: "EducationId",
                table: "m_Allowance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance",
                column: "EducationId",
                principalTable: "m_Education",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance");

            migrationBuilder.AlterColumn<long>(
                name: "EducationId",
                table: "m_Allowance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance",
                column: "EducationId",
                principalTable: "m_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
