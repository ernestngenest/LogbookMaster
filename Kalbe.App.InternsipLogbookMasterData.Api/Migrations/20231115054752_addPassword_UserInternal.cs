using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class addPassword_UserInternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserInternals",
                type: "citext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserInternals");
        }
    }
}
