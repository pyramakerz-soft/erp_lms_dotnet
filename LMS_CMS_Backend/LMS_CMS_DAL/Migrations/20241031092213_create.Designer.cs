﻿// <auto-generated />
using LMS_CMS_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    [DbContext(typeof(LMS_CMS_Context))]
    [Migration("20241031092213_create")]
    partial class create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMS_CMS_DAL.Models.Detailed_Permissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Master_Permission_ID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Master_Permission_ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Detailed_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("User_Name")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Employee_Role", b =>
                {
                    b.Property<int>("Employee_Id")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("Employee_Id", "Role_Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("Employee_Roles");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Master_Permissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Master_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Parent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("User_Name")
                        .IsUnique();

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Role_Detailed_Permissions", b =>
                {
                    b.Property<int>("Role_ID")
                        .HasColumnType("int");

                    b.Property<int>("Detailed_Permissions_ID")
                        .HasColumnType("int");

                    b.HasKey("Role_ID", "Detailed_Permissions_ID");

                    b.HasIndex("Detailed_Permissions_ID");

                    b.ToTable("Role_Detailed_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Parent_Id")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Parent_Id");

                    b.HasIndex("User_Name")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Detailed_Permissions", b =>
                {
                    b.HasOne("LMS_CMS_DAL.Models.Master_Permissions", "Master_Permissions")
                        .WithMany("Detailed_Permissions")
                        .HasForeignKey("Master_Permission_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Employee_Role", b =>
                {
                    b.HasOne("LMS_CMS_DAL.Models.Employee", "Employee")
                        .WithMany("Employee_Roles")
                        .HasForeignKey("Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_CMS_DAL.Models.Role", "Role")
                        .WithMany("Employee_Roles")
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Role_Detailed_Permissions", b =>
                {
                    b.HasOne("LMS_CMS_DAL.Models.Detailed_Permissions", "Detailed_Permissions")
                        .WithMany("Role_Detailed_Permissions")
                        .HasForeignKey("Detailed_Permissions_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_CMS_DAL.Models.Role", "Role")
                        .WithMany("Role_Detailed_Permissions")
                        .HasForeignKey("Role_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Detailed_Permissions");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Student", b =>
                {
                    b.HasOne("LMS_CMS_DAL.Models.Parent", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("Parent_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Detailed_Permissions", b =>
                {
                    b.Navigation("Role_Detailed_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Employee", b =>
                {
                    b.Navigation("Employee_Roles");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Master_Permissions", b =>
                {
                    b.Navigation("Detailed_Permissions");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Parent", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMS_CMS_DAL.Models.Role", b =>
                {
                    b.Navigation("Employee_Roles");

                    b.Navigation("Role_Detailed_Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}