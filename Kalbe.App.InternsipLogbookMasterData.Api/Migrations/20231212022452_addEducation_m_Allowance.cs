using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class addEducation_m_Allowance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "m_UserExternal",
                type: "citext",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EducationCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "m_Allowance",
                type: "citext",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "EducationCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "m_Allowance");
        }
    }
}
