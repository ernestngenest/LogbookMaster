using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class changeSchoolCodeTypeinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserInternalId",
                table: "m_UserRole",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SchoolCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmenetCode = table.Column<string>(type: "citext", nullable: false),
                    DepartmentName = table.Column<string>(type: "citext", nullable: true),
                    CreatedBy = table.Column<string>(type: "citext", nullable: true),
                    CreatedByName = table.Column<string>(type: "citext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "citext", nullable: true),
                    UpdatedByName = table.Column<string>(type: "citext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInternals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserPrincipalName = table.Column<string>(type: "citext", nullable: true),
                    NIK = table.Column<string>(type: "citext", nullable: true),
                    Name = table.Column<string>(type: "citext", nullable: true),
                    Email = table.Column<string>(type: "citext", nullable: true),
                    JobTitle = table.Column<string>(type: "citext", nullable: true),
                    DeptName = table.Column<string>(type: "citext", nullable: true),
                    CompCode = table.Column<string>(type: "citext", nullable: true),
                    CompName = table.Column<string>(type: "citext", nullable: true),
                    CreatedBy = table.Column<string>(type: "citext", nullable: true),
                    CreatedByName = table.Column<string>(type: "citext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "citext", nullable: true),
                    UpdatedByName = table.Column<string>(type: "citext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInternals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_UserRole_UserInternalId",
                table: "m_UserRole",
                column: "UserInternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_UserRole_UserInternals_UserInternalId",
                table: "m_UserRole",
                column: "UserInternalId",
                principalTable: "UserInternals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_UserRole_UserInternals_UserInternalId",
                table: "m_UserRole");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "UserInternals");

            migrationBuilder.DropIndex(
                name: "IX_m_UserRole_UserInternalId",
                table: "m_UserRole");

            migrationBuilder.DropColumn(
                name: "UserInternalId",
                table: "m_UserRole");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolCode",
                table: "m_UserExternal",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext");
        }
    }
}
