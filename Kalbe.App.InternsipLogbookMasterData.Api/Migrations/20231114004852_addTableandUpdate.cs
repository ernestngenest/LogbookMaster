using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class addTableandUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniversityName",
                table: "m_UserExternal",
                newName: "SchoolName");

            migrationBuilder.RenameColumn(
                name: "UniversityCode",
                table: "m_UserExternal",
                newName: "SchoolCode");

            migrationBuilder.RenameColumn(
                name: "Major",
                table: "m_UserExternal",
                newName: "Faculty");

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacultyCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InternshipPeriodMonth",
                table: "m_UserExternal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorName",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorUpn",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "FacultyCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "InternshipPeriodMonth",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "SupervisorName",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "SupervisorUpn",
                table: "m_UserExternal");

            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "m_UserExternal",
                newName: "UniversityName");

            migrationBuilder.RenameColumn(
                name: "SchoolCode",
                table: "m_UserExternal",
                newName: "UniversityCode");

            migrationBuilder.RenameColumn(
                name: "Faculty",
                table: "m_UserExternal",
                newName: "Major");
        }
    }
}
