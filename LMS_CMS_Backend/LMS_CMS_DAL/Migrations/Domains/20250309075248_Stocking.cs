using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class Stocking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocking",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stocking_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StockingDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentStock = table.Column<long>(type: "bigint", nullable: false),
                    ActualStock = table.Column<long>(type: "bigint", nullable: false),
                    TheDifference = table.Column<long>(type: "bigint", nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    StockingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockingDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockingDetails_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockingDetails_Stocking_StockingId",
                        column: x => x.StockingId,
                        principalTable: "Stocking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_DeletedByUserId",
                table: "Stocking",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_InsertedByUserId",
                table: "Stocking",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocking_UpdatedByUserId",
                table: "Stocking",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_ShopItemID",
                table: "StockingDetails",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_StockingDetails_StockingId",
                table: "StockingDetails",
                column: "StockingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockingDetails");

            migrationBuilder.DropTable(
                name: "Stocking");
        }
    }
}
