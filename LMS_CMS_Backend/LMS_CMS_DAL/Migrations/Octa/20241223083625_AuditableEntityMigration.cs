using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AuditableEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Octa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedByUserId",
                table: "Octa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertedAt",
                table: "Octa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InsertedByUserId",
                table: "Octa",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Octa",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Octa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedByUserId",
                table: "Octa",
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

            migrationBuilder.CreateIndex(
                name: "IX_Octa_DeletedByUserId",
                table: "Octa",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Octa_InsertedByUserId",
                table: "Octa",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Octa_UpdatedByUserId",
                table: "Octa",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Octa_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Octa_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Octa_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Octa_Octa_DeletedByUserId",
                table: "Octa",
                column: "DeletedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Octa_Octa_InsertedByUserId",
                table: "Octa",
                column: "InsertedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Octa_Octa_UpdatedByUserId",
                table: "Octa",
                column: "UpdatedByUserId",
                principalTable: "Octa",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Octa_DeletedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Octa_InsertedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Octa_UpdatedByUserId",
                table: "Domains");

            migrationBuilder.DropForeignKey(
                name: "FK_Octa_Octa_DeletedByUserId",
                table: "Octa");

            migrationBuilder.DropForeignKey(
                name: "FK_Octa_Octa_InsertedByUserId",
                table: "Octa");

            migrationBuilder.DropForeignKey(
                name: "FK_Octa_Octa_UpdatedByUserId",
                table: "Octa");

            migrationBuilder.DropIndex(
                name: "IX_Octa_DeletedByUserId",
                table: "Octa");

            migrationBuilder.DropIndex(
                name: "IX_Octa_InsertedByUserId",
                table: "Octa");

            migrationBuilder.DropIndex(
                name: "IX_Octa_UpdatedByUserId",
                table: "Octa");

            migrationBuilder.DropIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "InsertedByUserId",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Octa");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Octa");

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
        }
    }
}
