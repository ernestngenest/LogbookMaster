using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class supervisorToMentor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupervisorUpn",
                table: "m_UserExternal",
                newName: "MentorUpn");

            migrationBuilder.RenameColumn(
                name: "SupervisorName",
                table: "m_UserExternal",
                newName: "MentorName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MentorUpn",
                table: "m_UserExternal",
                newName: "SupervisorUpn");

            migrationBuilder.RenameColumn(
                name: "MentorName",
                table: "m_UserExternal",
                newName: "SupervisorName");
        }
    }
}
