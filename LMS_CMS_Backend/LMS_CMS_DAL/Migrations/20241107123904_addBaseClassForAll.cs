using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addBaseClassForAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Role_Detailes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Role_Detailes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Role_Detailes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Role_Detailes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Role_Detailes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pyramakerz",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pyramakerz",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Pyramakerz",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Parents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Parents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Parents",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Parents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Parents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Pages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Domains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Domains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Domains",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Domains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Domains",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Domain_Page_Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Domain_Page_Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Domain_Page_Details",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Domain_Page_Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Domain_Page_Details",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeletedByUserId",
                table: "Students",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InsertedByUserId",
                table: "Students",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UpdatedByUserId",
                table: "Students",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedByUserId",
                table: "Roles",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_InsertedByUserId",
                table: "Roles",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_DeletedByUserId",
                table: "Role_Detailes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_InsertedByUserId",
                table: "Role_Detailes",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_UpdatedByUserId",
                table: "Role_Detailes",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_DeletedByUserId",
                table: "Pyramakerz",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_InsertedByUserId",
                table: "Pyramakerz",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_UpdatedByUserId",
                table: "Pyramakerz",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_DeletedByUserId",
                table: "Parents",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_InsertedByUserId",
                table: "Parents",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UpdatedByUserId",
                table: "Parents",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DeletedByUserId",
                table: "Pages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_InsertedByUserId",
                table: "Pages",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UpdatedByUserId",
                table: "Pages",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeletedByUserId",
                table: "Employees",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_InsertedByUserId",
                table: "Employees",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedByUserId",
                table: "Employees",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_DeletedByUserId",
                table: "Domain_Page_Details",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_InsertedByUserId",
                table: "Domain_Page_Details",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_UpdatedByUserId",
                table: "Domain_Page_Details",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_DeletedByUserId",
                table: "Domain_Page_Details",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_InsertedByUserId",
                table: "Domain_Page_Details",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domain_Page_Details_Employees_UpdatedByUserId",
                table: "Domain_Page_Details",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Employees_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_DeletedByUserId",
                table: "Employees",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_InsertedByUserId",
                table: "Employees",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_UpdatedByUserId",
                table: "Employees",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_DeletedByUserId",
                table: "Pages",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_InsertedByUserId",
                table: "Pages",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Employees_UpdatedByUserId",
                table: "Pages",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_DeletedByUserId",
                table: "Parents",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_InsertedByUserId",
                table: "Parents",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Employees_UpdatedByUserId",
                table: "Parents",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_DeletedByUserId",
                table: "Pyramakerz",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_InsertedByUserId",
                table: "Pyramakerz",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pyramakerz_Employees_UpdatedByUserId",
                table: "Pyramakerz",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_DeletedByUserId",
                table: "Role_Detailes",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_InsertedByUserId",
                table: "Role_Detailes",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Detailes_Employees_UpdatedByUserId",
                table: "Role_Detailes",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_DeletedByUserId",
                table: "Roles",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_InsertedByUserId",
                table: "Roles",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Employees_UpdatedByUserId",
                table: "Roles",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_DeletedByUserId",
                table: "Students",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_InsertedByUserId",
                table: "Students",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Employees_UpdatedByUserId",
                table: "Students",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Employees_DeletedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Employees_InsertedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domain_Page_Details_Employees_UpdatedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Employees_DeletedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Employees_InsertedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Employees_UpdatedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_DeletedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_InsertedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_UpdatedByUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Employees_UpdatedByUserId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_DeletedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_InsertedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Employees_UpdatedByUserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropForeignKey(
                name: "FK_Pyramakerz_Employees_UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_DeletedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_InsertedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Detailes_Employees_UpdatedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_DeletedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_InsertedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Employees_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_DeletedByUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_InsertedByUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Employees_UpdatedByUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeletedByUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InsertedByUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UpdatedByUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DeletedByUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_InsertedByUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_DeletedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_InsertedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Role_Detailes_UpdatedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Pyramakerz_UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropIndex(
                name: "IX_Parents_DeletedByUserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_InsertedByUserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UpdatedByUserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Pages_DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_UpdatedByUserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DeletedByUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_InsertedByUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UpdatedByUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_DeletedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_InsertedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropIndex(
                name: "IX_Domain_Page_Details_UpdatedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Role_Detailes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Pyramakerz");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Domain_Page_Details");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Domain_Page_Details");
        }
    }
}
