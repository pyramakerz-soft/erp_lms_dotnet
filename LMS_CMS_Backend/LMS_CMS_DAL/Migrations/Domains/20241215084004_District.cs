using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class District : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusRestrict_BusRestrictID",
                table: "Bus");

            migrationBuilder.DropTable(
                name: "BusRestrict");


            migrationBuilder.RenameColumn(
                name: "BusRestrictID",
                table: "Bus",
                newName: "BusDistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_BusRestrictID",
                table: "Bus",
                newName: "IX_Bus_BusDistrictID");

            migrationBuilder.CreateTable(
                name: "BusDistrict",
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
                    table.PrimaryKey("PK_BusDistrict", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusDistrict_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BusDistrict_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BusDistrict_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_DeletedByUserId",
                table: "BusDistrict",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_InsertedByUserId",
                table: "BusDistrict",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDistrict_UpdatedByUserId",
                table: "BusDistrict",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusDistrict_BusDistrictID",
                table: "Bus",
                column: "BusDistrictID",
                principalTable: "BusDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusDistrict_BusDistrictID",
                table: "Bus");

            migrationBuilder.DropTable(
                name: "BusDistrict");


            migrationBuilder.RenameColumn(
                name: "BusDistrictID",
                table: "Bus",
                newName: "BusRestrictID");

            migrationBuilder.RenameIndex(
                name: "IX_Bus_BusDistrictID",
                table: "Bus",
                newName: "IX_Bus_BusRestrictID");

            migrationBuilder.CreateTable(
                name: "BusRestrict",
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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByOctaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRestrict", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusRestrict_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BusRestrict_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BusRestrict_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusRestrict_BusRestrictID",
                table: "Bus",
                column: "BusRestrictID",
                principalTable: "BusRestrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
