using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "m_UserExternal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "m_UserExternal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateTable(
                name: "m_Approval",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationCode = table.Column<string>(type: "citext", nullable: false),
                    ModuleCode = table.Column<string>(type: "citext", nullable: true),
                    Role = table.Column<string>(type: "citext", nullable: true),
                    ApprovalLevel = table.Column<int>(type: "integer", nullable: false),
                    isAllMustApprove = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_m_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_Approval)",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovalId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentNumber = table.Column<string>(type: "citext", nullable: true),
                    EmailPIC = table.Column<string>(type: "citext", nullable: true),
                    NamePIC = table.Column<string>(type: "citext", nullable: true),
                    PIC = table.Column<string>(type: "citext", nullable: true),
                    ApprovalLine = table.Column<int>(type: "integer", nullable: false),
                    NeedApprove = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "citext", nullable: true),
                    DueDate = table.Column<string>(type: "citext", nullable: true),
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
                    table.PrimaryKey("PK_t_Approval)", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_Approval)_m_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "m_Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_Approval)_ApprovalId",
                table: "t_Approval)",
                column: "ApprovalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_Approval)");

            migrationBuilder.DropTable(
                name: "m_Approval");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "RoleCode",
                table: "m_UserExternal");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "m_UserExternal");
        }
    }
}
