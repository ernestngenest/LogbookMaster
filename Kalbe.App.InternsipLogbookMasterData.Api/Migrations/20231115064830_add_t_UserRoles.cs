using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class add_t_UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_Role_m_UserInternal_UserInternalId",
                table: "m_Role");

            migrationBuilder.DropIndex(
                name: "IX_m_Role_UserInternalId",
                table: "m_Role");

            migrationBuilder.DropColumn(
                name: "RoleCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "UserInternalId",
                table: "m_Role");

            migrationBuilder.CreateTable(
                name: "t_UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserPrincipalName = table.Column<string>(type: "citext", nullable: false),
                    Email = table.Column<string>(type: "citext", nullable: true),
                    Name = table.Column<string>(type: "citext", nullable: false),
                    RoleCode = table.Column<string>(type: "citext", nullable: false),
                    RoleName = table.Column<string>(type: "citext", nullable: false),
                    UserInternalId = table.Column<long>(type: "bigint", nullable: true),
                    UserExternalId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_t_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_UserRole_m_UserExternal_UserExternalId",
                        column: x => x.UserExternalId,
                        principalTable: "m_UserExternal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_t_UserRole_m_UserInternal_UserInternalId",
                        column: x => x.UserInternalId,
                        principalTable: "m_UserInternal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_UserRole_UserExternalId",
                table: "t_UserRole",
                column: "UserExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_UserRole_UserInternalId",
                table: "t_UserRole",
                column: "UserInternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_UserRole");

            migrationBuilder.AddColumn<string>(
                name: "RoleCode",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "m_UserExternal",
                type: "citext",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserInternalId",
                table: "m_Role",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_Role_UserInternalId",
                table: "m_Role",
                column: "UserInternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_Role_m_UserInternal_UserInternalId",
                table: "m_Role",
                column: "UserInternalId",
                principalTable: "m_UserInternal",
                principalColumn: "Id");
        }
    }
}
