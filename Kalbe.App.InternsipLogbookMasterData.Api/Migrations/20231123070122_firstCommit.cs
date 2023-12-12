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
                name: "m_Allowance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkType = table.Column<string>(type: "citext", nullable: false),
                    AllowanceFee = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_m_Allowance", x => x.Id);
                });

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
                name: "m_Department",
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
                    table.PrimaryKey("PK_m_Department", x => x.Id);
                });

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
                name: "m_Role",
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
                    table.PrimaryKey("PK_m_Role", x => x.Id);
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
                    UserPrincipalName = table.Column<string>(type: "citext", nullable: false),
                    Name = table.Column<string>(type: "citext", nullable: false),
                    SchoolCode = table.Column<string>(type: "citext", nullable: false),
                    SchoolName = table.Column<string>(type: "citext", nullable: false),
                    FacultyCode = table.Column<string>(type: "citext", nullable: true),
                    Faculty = table.Column<string>(type: "citext", nullable: false),
                    DeptCode = table.Column<string>(type: "citext", nullable: true),
                    Dept = table.Column<string>(type: "citext", nullable: false),
                    Password = table.Column<string>(type: "citext", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "citext", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SupervisorUpn = table.Column<string>(type: "citext", nullable: true),
                    SupervisorName = table.Column<string>(type: "citext", nullable: true),
                    CompanyCode = table.Column<string>(type: "citext", nullable: true),
                    CompanyName = table.Column<string>(type: "citext", nullable: true),
                    InternshipPeriodMonth = table.Column<int>(type: "integer", nullable: false),
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
                name: "m_UserInternal",
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
                    Password = table.Column<string>(type: "citext", nullable: true),
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
                    table.PrimaryKey("PK_m_UserInternal", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "t_Approval",
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
                    ApproveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUpn = table.Column<string>(type: "citext", nullable: true),
                    CreatedByEmail = table.Column<string>(type: "citext", nullable: true),
                    CreatedByName = table.Column<string>(type: "citext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUpn = table.Column<string>(type: "citext", nullable: true),
                    UpdatedByEmail = table.Column<string>(type: "citext", nullable: true),
                    UpdatedByName = table.Column<string>(type: "citext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "citext", nullable: true),
                    UpdatedBy = table.Column<string>(type: "citext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_Approval_m_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "m_Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserPrincipalName = table.Column<string>(type: "citext", nullable: true),
                    Email = table.Column<string>(type: "citext", nullable: true),
                    Name = table.Column<string>(type: "citext", nullable: true),
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
                name: "IX_m_Faculty_FacultyCode_FacultyName",
                table: "m_Faculty",
                columns: new[] { "FacultyCode", "FacultyName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_Role_RoleCode_RoleName",
                table: "m_Role",
                columns: new[] { "RoleCode", "RoleName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_m_School_SchoolCode_SchoolName",
                table: "m_School",
                columns: new[] { "SchoolCode", "SchoolName" },
                unique: true,
                filter: "\"IsDeleted\" = False");

            migrationBuilder.CreateIndex(
                name: "IX_t_Approval_ApprovalId",
                table: "t_Approval",
                column: "ApprovalId");

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
                name: "m_Allowance");

            migrationBuilder.DropTable(
                name: "m_Department");

            migrationBuilder.DropTable(
                name: "m_Faculty");

            migrationBuilder.DropTable(
                name: "m_Role");

            migrationBuilder.DropTable(
                name: "m_School");

            migrationBuilder.DropTable(
                name: "t_Approval");

            migrationBuilder.DropTable(
                name: "t_Logger");

            migrationBuilder.DropTable(
                name: "t_UserRole");

            migrationBuilder.DropTable(
                name: "m_Approval");

            migrationBuilder.DropTable(
                name: "m_UserExternal");

            migrationBuilder.DropTable(
                name: "m_UserInternal");
        }
    }
}
