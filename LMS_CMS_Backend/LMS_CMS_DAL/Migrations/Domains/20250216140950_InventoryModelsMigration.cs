using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InventoryModelsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_InventoryCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Store", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Store_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Store_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Store_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InventorySubCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InventoryCategoriesID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InventorySubCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventorySubCategories_InventoryCategories_InventoryCategoriesID",
                        column: x => x.InventoryCategoriesID,
                        principalTable: "InventoryCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCash = table.Column<bool>(type: "bit", nullable: false),
                    IsVisa = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Remaining = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
                    StudentID = table.Column<long>(type: "bigint", nullable: false),
                    SaveID = table.Column<long>(type: "bigint", nullable: true),
                    BankID = table.Column<long>(type: "bigint", nullable: true),
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
                name: "StoreCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryCategoriesID = table.Column<long>(type: "bigint", nullable: false),
                    StoreID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_StoreCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StoreCategories_InventoryCategories_InventoryCategoriesID",
                        column: x => x.InventoryCategoriesID,
                        principalTable: "InventoryCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopItem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<float>(type: "real", nullable: false),
                    SalesPrice = table.Column<float>(type: "real", nullable: false),
                    VATForForeign = table.Column<float>(type: "real", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    AvailableInShop = table.Column<bool>(type: "bit", nullable: false),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderID = table.Column<long>(type: "bigint", nullable: false),
                    InventorySubCategoriesID = table.Column<long>(type: "bigint", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    GradeID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShopItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItem_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_Grade_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_InventorySubCategories_InventorySubCategoriesID",
                        column: x => x.InventorySubCategoriesID,
                        principalTable: "InventorySubCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopItem_School_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesItem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ShopItemColor",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShopItemColor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemColor_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopItemSize",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopItemID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShopItemSize", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ShopItemSize_ShopItem_ShopItemID",
                        column: x => x.ShopItemID,
                        principalTable: "ShopItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesItemAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesItemID = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_SalesItemAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SalesItemAttachment_SalesItem_SalesItemID",
                        column: x => x.SalesItemID,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_DeletedByUserId",
                table: "InventoryCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_InsertedByUserId",
                table: "InventoryCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryCategories_UpdatedByUserId",
                table: "InventoryCategories",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_DeletedByUserId",
                table: "InventorySubCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_InsertedByUserId",
                table: "InventorySubCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_InventoryCategoriesID",
                table: "InventorySubCategories",
                column: "InventoryCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySubCategories_UpdatedByUserId",
                table: "InventorySubCategories",
                column: "UpdatedByUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_DeletedByUserId",
                table: "SalesItemAttachment",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_InsertedByUserId",
                table: "SalesItemAttachment",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_SalesItemID",
                table: "SalesItemAttachment",
                column: "SalesItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemAttachment_UpdatedByUserId",
                table: "SalesItemAttachment",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_DeletedByUserId",
                table: "ShopItem",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_GenderID",
                table: "ShopItem",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_GradeID",
                table: "ShopItem",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_InsertedByUserId",
                table: "ShopItem",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_InventorySubCategoriesID",
                table: "ShopItem",
                column: "InventorySubCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_SchoolID",
                table: "ShopItem",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_UpdatedByUserId",
                table: "ShopItem",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_DeletedByUserId",
                table: "ShopItemColor",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_InsertedByUserId",
                table: "ShopItemColor",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_ShopItemID",
                table: "ShopItemColor",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemColor_UpdatedByUserId",
                table: "ShopItemColor",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_DeletedByUserId",
                table: "ShopItemSize",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_InsertedByUserId",
                table: "ShopItemSize",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_ShopItemID",
                table: "ShopItemSize",
                column: "ShopItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItemSize_UpdatedByUserId",
                table: "ShopItemSize",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_DeletedByUserId",
                table: "Store",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_InsertedByUserId",
                table: "Store",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_UpdatedByUserId",
                table: "Store",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_DeletedByUserId",
                table: "StoreCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_InsertedByUserId",
                table: "StoreCategories",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_InventoryCategoriesID",
                table: "StoreCategories",
                column: "InventoryCategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_StoreID",
                table: "StoreCategories",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_UpdatedByUserId",
                table: "StoreCategories",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesItemAttachment");

            migrationBuilder.DropTable(
                name: "ShopItemColor");

            migrationBuilder.DropTable(
                name: "ShopItemSize");

            migrationBuilder.DropTable(
                name: "StoreCategories");

            migrationBuilder.DropTable(
                name: "SalesItem");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ShopItem");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "InventorySubCategories");

            migrationBuilder.DropTable(
                name: "InventoryCategories");
        }
    }
}
