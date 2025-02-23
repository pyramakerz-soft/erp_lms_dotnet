using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItemAttachment_SalesItem_SalesItemID",
                table: "SalesItemAttachment");

            migrationBuilder.DropTable(
                name: "SalesItem");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.CreateTable(
                name: "InventoryFlags",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryFlags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMaster",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsVisa = table.Column<bool>(type: "bit", nullable: false),
                    CashAmount = table.Column<int>(type: "int", nullable: false),
                    VisaAmount = table.Column<int>(type: "int", nullable: false),
                    Remaining = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    SaveID = table.Column<long>(type: "bigint", nullable: true),
                    BankID = table.Column<long>(type: "bigint", nullable: true),
                    FlagId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InventoryMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryMaster_InventoryFlags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "InventoryFlags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Saves_SaveID",
                        column: x => x.SaveID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMaster_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    SalesID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InventoryDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryDetails_InventoryMaster_SalesID",
                        column: x => x.SalesID,
                        principalTable: "InventoryMaster",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_DeletedByUserId",
                table: "InventoryDetails",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_InsertedByUserId",
                table: "InventoryDetails",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_SalesID",
                table: "InventoryDetails",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_ShopItemID",
                table: "InventoryDetails",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_UpdatedByUserId",
                table: "InventoryDetails",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_BankID",
                table: "InventoryMaster",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_DeletedByUserId",
                table: "InventoryMaster",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_FlagId",
                table: "InventoryMaster",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_InsertedByUserId",
                table: "InventoryMaster",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_SaveID",
                table: "InventoryMaster",
                column: "SaveID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StoreID",
                table: "InventoryMaster",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_StudentID",
                table: "InventoryMaster",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMaster_UpdatedByUserId",
                table: "InventoryMaster",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_SalesItemID",
                table: "SalesItemAttachment",
                column: "SalesItemID",
                principalTable: "InventoryDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItemAttachment_InventoryDetails_SalesItemID",
                table: "SalesItemAttachment");

            migrationBuilder.DropTable(
                name: "InventoryDetails");

            migrationBuilder.DropTable(
                name: "InventoryMaster");

            migrationBuilder.DropTable(
                name: "InventoryFlags");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankID = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    SaveID = table.Column<long>(type: "bigint", nullable: true),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashAmount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsVisa = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remaining = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    VisaAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sales_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sales_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sales_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sales_Saves_SaveID",
                        column: x => x.SaveID,
                        principalTable: "Saves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesItem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    SalesID = table.Column<long>(type: "bigint", nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertedByOctaId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SalesItem_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItem_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItem_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItem_Sales_SalesID",
                        column: x => x.SalesID,
                        principalTable: "Sales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesItem_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BankID",
                table: "Sales",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DeletedByUserId",
                table: "Sales",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_InsertedByUserId",
                table: "Sales",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaveID",
                table: "Sales",
                column: "SaveID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StoreID",
                table: "Sales",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StudentID",
                table: "Sales",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UpdatedByUserId",
                table: "Sales",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_DeletedByUserId",
                table: "SalesItem",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_InsertedByUserId",
                table: "SalesItem",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_SalesID",
                table: "SalesItem",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_ShopItemID",
                table: "SalesItem",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_UpdatedByUserId",
                table: "SalesItem",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItemAttachment_SalesItem_SalesItemID",
                table: "SalesItemAttachment",
                column: "SalesItemID",
                principalTable: "SalesItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
