 using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class RemovePromoCodeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_PromoCode_PromoCodeID",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "PromoCode");

            migrationBuilder.DropIndex(
                name: "IX_Cart_PromoCodeID",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "PromoCodeID",
                table: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PromoCodeID",
                table: "Cart",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PromoCode",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCode", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromoCode_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PromoCode_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PromoCode_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_PromoCodeID",
                table: "Cart",
                column: "PromoCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_DeletedByUserId",
                table: "PromoCode",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_InsertedByUserId",
                table: "PromoCode",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_UpdatedByUserId",
                table: "PromoCode",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_PromoCode_PromoCodeID",
                table: "Cart",
                column: "PromoCodeID",
                principalTable: "PromoCode",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
