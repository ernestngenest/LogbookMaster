using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class firstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "m_Faculty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacultyCode = table.Column<string>(type: "citext", nullable: false),
                    FacultyName = table.Column<string>(type: "citext", nullable: false),
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
                    table.PrimaryKey("PK_m_Faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_School",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolCode = table.Column<string>(type: "citext", nullable: false),
                    SchoolName = table.Column<string>(type: "citext", nullable: false),
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
                    table.PrimaryKey("PK_m_School", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_UserExternal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "citext", nullable: false),
                    UniversityCode = table.Column<long>(type: "bigint", nullable: false),
                    UniversityName = table.Column<string>(type: "citext", nullable: false),
                    Major = table.Column<string>(type: "citext", nullable: false),
                    Dept = table.Column<string>(type: "citext", nullable: false),
                    Password = table.Column<string>(type: "citext", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "citext", nullable: true),
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
                    table.PrimaryKey("PK_m_UserExternal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleCode = table.Column<string>(type: "citext", nullable: false),
                    RoleName = table.Column<string>(type: "citext", nullable: false),
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
                    table.PrimaryKey("PK_m_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_Logger",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppCode = table.Column<string>(type: "citext", nullable: true),
                    ModuleCode = table.Column<string>(type: "citext", nullable: true),
                    DocumentNumber = table.Column<string>(type: "citext", nullable: true),
                    Activity = table.Column<string>(type: "citext", nullable: true),
                    CompanyId = table.Column<string>(type: "citext", nullable: true),
                    LogType = table.Column<string>(type: "citext", nullable: true),
                    Message = table.Column<string>(type: "citext", nullable: true),
                    PayLoad = table.Column<string>(type: "citext", nullable: true),
                    PayLoadType = table.Column<string>(type: "citext", nullable: true),
                    ExternalEntity = table.Column<string>(type: "citext", nullable: true),
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
                    table.PrimaryKey("PK_t_Logger", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_Faculty_FacultyCode_FacultyName",
                table: "m_Faculty",
                columns: new[] { "FacultyCode", "FacultyName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_School_SchoolCode_SchoolName",
                table: "m_School",
                columns: new[] { "SchoolCode", "SchoolName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_UserRole_RoleCode_RoleName",
                table: "m_UserRole",
                columns: new[] { "RoleCode", "RoleName" },
                unique: true,
                filter: "\"IsDeleted\" = False");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_Faculty");

            migrationBuilder.DropTable(
                name: "m_School");

            migrationBuilder.DropTable(
                name: "m_UserExternal");

            migrationBuilder.DropTable(
                name: "m_UserRole");

            migrationBuilder.DropTable(
                name: "t_Logger");
        }
    }
}
