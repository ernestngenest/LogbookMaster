using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    public partial class add_m_Education : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "m_Allowance");

            migrationBuilder.AddColumn<long>(
                name: "EducationId",
                table: "m_Allowance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "m_Education",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducationCode = table.Column<string>(type: "citext", nullable: true),
                    EducationName = table.Column<string>(type: "citext", nullable: true),
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
                    table.PrimaryKey("PK_m_Education", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_Allowance_EducationId",
                table: "m_Allowance",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance",
                column: "EducationId",
                principalTable: "m_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_Allowance_m_Education_EducationId",
                table: "m_Allowance");

            migrationBuilder.DropTable(
                name: "m_Education");

            migrationBuilder.DropIndex(
                name: "IX_m_Allowance_EducationId",
                table: "m_Allowance");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "m_Allowance");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "m_Allowance",
                type: "citext",
                nullable: false,
                defaultValue: "");
        }
    }
}
