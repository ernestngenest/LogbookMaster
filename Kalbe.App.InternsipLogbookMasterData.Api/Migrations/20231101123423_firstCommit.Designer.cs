﻿// <auto-generated />
using System;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kalbe.App.InternsipLogbookMasterData.Api.Migrations
{
    [DbContext(typeof(InternsipLogbookMasterDataDataContext))]
    [Migration("20231101123423_firstCommit")]
    partial class firstCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "citext");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<string>("CreatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Dept")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("citext");

                    b.Property<string>("Status")
                        .HasColumnType("citext");

                    b.Property<long>("UniversityCode")
                        .HasColumnType("bigint");

                    b.Property<string>("UniversityName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("citext");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("citext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("m_UserExternal");
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

                    b.ToTable("m_UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
