using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class userRoletoRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInternals",
                table: "UserInternals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allowances",
                table: "Allowances");

            migrationBuilder.RenameTable(
                name: "UserInternals",
                newName: "m_UserInternal");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "m_Department");

            migrationBuilder.RenameTable(
                name: "Allowances",
                newName: "m_Allowance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_UserInternal",
                table: "m_UserInternal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_Department",
                table: "m_Department",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_Allowance",
                table: "m_Allowance",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "m_Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleCode = table.Column<string>(type: "citext", nullable: false),
                    RoleName = table.Column<string>(type: "citext", nullable: false),
                    UserInternalId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_m_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_Role_m_UserInternal_UserInternalId",
                        column: x => x.UserInternalId,
                        principalTable: "m_UserInternal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_Role_RoleCode_RoleName",
                table: "m_Role",
                columns: new[] { "RoleCode", "RoleName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_Role_UserInternalId",
                table: "m_Role",
                column: "UserInternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_UserInternal",
                table: "m_UserInternal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_Department",
                table: "m_Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_Allowance",
                table: "m_Allowance");

            migrationBuilder.RenameTable(
                name: "m_UserInternal",
                newName: "UserInternals");

            migrationBuilder.RenameTable(
                name: "m_Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "m_Allowance",
                newName: "Allowances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInternals",
                table: "UserInternals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allowances",
                table: "Allowances",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "m_UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(type: "citext", nullable: true),
                    CreatedByName = table.Column<string>(type: "citext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RoleCode = table.Column<string>(type: "citext", nullable: false),
                    RoleName = table.Column<string>(type: "citext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "citext", nullable: true),
                    UpdatedByName = table.Column<string>(type: "citext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserInternalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_UserRole_UserInternals_UserInternalId",
                        column: x => x.UserInternalId,
                        principalTable: "UserInternals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_UserRole_RoleCode_RoleName",
                table: "m_UserRole",
                columns: new[] { "RoleCode", "RoleName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_UserRole_UserInternalId",
                table: "m_UserRole",
                column: "UserInternalId");
        }
    }
}
