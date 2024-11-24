using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntityInBus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusType",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusStudent",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusStudent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusStudent",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusStatus",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusStatus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusRestrict",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusRestrict",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusRestrict",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusRestrict",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusRestrict",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusCompany",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusCompany",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusCompany",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusCompany",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusCompany",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BusCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "BusCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BusCategory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BusCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "BusCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bus",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Bus",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusType_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_InsertedByUserId",
                table: "BusType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_UpdatedByUserId",
                table: "BusType",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_DeletedByUserId",
                table: "BusStudent",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_InsertedByUserId",
                table: "BusStudent",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStudent_UpdatedByUserId",
                table: "BusStudent",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_InsertedByUserId",
                table: "BusStatus",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStatus_UpdatedByUserId",
                table: "BusStatus",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_DeletedByUserId",
                table: "BusRestrict",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_InsertedByUserId",
                table: "BusRestrict",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusRestrict_UpdatedByUserId",
                table: "BusRestrict",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompany_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_InsertedByUserId",
                table: "BusCategory",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusCategory_UpdatedByUserId",
                table: "BusCategory",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_InsertedByUserId",
                table: "Bus",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_UpdatedByUserId",
                table: "Bus",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_InsertedByUserId",
                table: "Bus",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_Employees_UpdatedByUserId",
                table: "Bus",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_InsertedByUserId",
                table: "BusCategory",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCategory_Employees_UpdatedByUserId",
                table: "BusCategory",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Employees_InsertedByUserId",
                table: "BusRestrict",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusRestrict_Employees_UpdatedByUserId",
                table: "BusRestrict",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_InsertedByUserId",
                table: "BusStatus",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStatus_Employees_UpdatedByUserId",
                table: "BusStatus",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_DeletedByUserId",
                table: "BusStudent",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_InsertedByUserId",
                table: "BusStudent",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStudent_Employees_UpdatedByUserId",
                table: "BusStudent",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType",
                column: "DeletedByUserId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_InsertedByUserId",
                table: "BusType",
                column: "InsertedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusType_Employees_UpdatedByUserId",
                table: "BusType",
                column: "UpdatedByUserId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_InsertedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bus_Employees_UpdatedByUserId",
                table: "Bus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_InsertedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCategory_Employees_UpdatedByUserId",
                table: "BusCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusCompany_Employees_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Employees_DeletedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Employees_InsertedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusRestrict_Employees_UpdatedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_InsertedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStatus_Employees_UpdatedByUserId",
                table: "BusStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_DeletedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_InsertedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusStudent_Employees_UpdatedByUserId",
                table: "BusStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_InsertedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_BusType_Employees_UpdatedByUserId",
                table: "BusType");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_BusType_DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_InsertedByUserId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusType_UpdatedByUserId",
                table: "BusType");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_DeletedByUserId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_InsertedByUserId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStudent_UpdatedByUserId",
                table: "BusStudent");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_InsertedByUserId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusStatus_UpdatedByUserId",
                table: "BusStatus");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_DeletedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_InsertedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusRestrict_UpdatedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCompany_UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_InsertedByUserId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_BusCategory_UpdatedByUserId",
                table: "BusCategory");

            migrationBuilder.DropIndex(
                name: "IX_Bus_DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_InsertedByUserId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_UpdatedByUserId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusStudent");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusStatus");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusRestrict");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusCompany");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "BusCategory");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Bus");

            migrationBuilder.AddForeignKey(
                name: "FK_BusCompany_Domains_DomainId",
                table: "BusCompany",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BusCompany_BusCompanyID",
                table: "Employees",
                column: "BusCompanyID",
                principalTable: "BusCompany",
                principalColumn: "ID");
        }
    }
}
