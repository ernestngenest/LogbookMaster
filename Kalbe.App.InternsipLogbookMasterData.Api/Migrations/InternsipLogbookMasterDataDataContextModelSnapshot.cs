﻿// <auto-generated />
using System;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    [DbContext(typeof(InternsipLogbookMasterDataDataContext))]
    partial class InternsipLogbookMasterDataDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "citext");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Allowance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AllowanceFee")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("m_Allowance");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Approval", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ApplicationCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("ApprovalLevel")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModuleCode")
                        .HasColumnType("citext");

                    b.Property<string>("Role")
                        .HasColumnType("citext");

                    b.Property<string>("Status")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("isAllMustApprove")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("m_Approval");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.ApprovalDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ApprovalId")
                        .HasColumnType("bigint");

                    b.Property<int>("ApprovalLine")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("citext");

                    b.Property<string>("DueDate")
                        .HasColumnType("citext");

                    b.Property<string>("EmailPIC")
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("NamePIC")
                        .HasColumnType("citext");

                    b.Property<bool>("NeedApprove")
                        .HasColumnType("boolean");

                    b.Property<string>("Notes")
                        .HasColumnType("citext");

                    b.Property<string>("PIC")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.ToTable("t_Approval");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons.Logger", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Activity")
                        .HasColumnType("citext");

                    b.Property<string>("AppCode")
                        .HasColumnType("citext");

                    b.Property<string>("CompanyId")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("citext");

                    b.Property<string>("ExternalEntity")
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LogType")
                        .HasColumnType("citext");

                    b.Property<string>("Message")
                        .HasColumnType("citext");

                    b.Property<string>("ModuleCode")
                        .HasColumnType("citext");

                    b.Property<string>("PayLoad")
                        .HasColumnType("citext");

                    b.Property<string>("PayLoadType")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("t_Logger");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DepartmenetCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("m_Department");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FacultyCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("FacultyCode", "FacultyName")
                        .IsUnique()
                        .HasFilter("\"IsDeleted\" = False");

                    b.ToTable("m_Faculty");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RoleCode", "RoleName")
                        .IsUnique()
                        .HasFilter("\"IsDeleted\" = False");

                    b.ToTable("m_Role");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.School", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("SchoolCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("SchoolCode", "SchoolName")
                        .IsUnique()
                        .HasFilter("\"IsDeleted\" = False");

                    b.ToTable("m_School");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserExternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CompanyCode")
                        .HasColumnType("citext");

                    b.Property<string>("CompanyName")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Dept")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("DeptCode")
                        .HasColumnType("citext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("FacultyCode")
                        .HasColumnType("citext");

                    b.Property<int>("InternshipPeriodMonth")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("citext");

                    b.Property<string>("SchoolCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Status")
                        .HasColumnType("citext");

                    b.Property<string>("SupervisorName")
                        .HasColumnType("citext");

                    b.Property<string>("SupervisorUpn")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserPrincipalName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("m_UserExternal");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CompCode")
                        .HasColumnType("citext");

                    b.Property<string>("CompName")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeptName")
                        .HasColumnType("citext");

                    b.Property<string>("Email")
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("JobTitle")
                        .HasColumnType("citext");

                    b.Property<string>("NIK")
                        .HasColumnType("citext");

                    b.Property<string>("Name")
                        .HasColumnType("citext");

                    b.Property<string>("Password")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserPrincipalName")
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("m_UserInternal");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("UserExternalId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserInternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserPrincipalName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.HasIndex("UserExternalId")
                        .IsUnique();

                    b.HasIndex("UserInternalId");

                    b.ToTable("t_UserRole");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.ApprovalDetail", b =>
                {
                    b.HasOne("Kalbe.App.InternsipLogbookMasterData.Api.Models.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserRole", b =>
                {
                    b.HasOne("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserExternal", "UserExternal")
                        .WithOne("UserRole")
                        .HasForeignKey("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserRole", "UserExternalId");

                    b.HasOne("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal", "UserInternal")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserInternalId");

                    b.Navigation("UserExternal");

                    b.Navigation("UserInternal");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserExternal", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Kalbe.App.InternsipLogbookMasterData.Api.Models.UserInternal", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
