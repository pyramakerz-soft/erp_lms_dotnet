using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class CreateOctaDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iso3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numcode = table.Column<int>(type: "int", nullable: false),
                    PhoneCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Octa",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Arabic_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Octa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Octa_Octa_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Octa_Octa_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Octa_Octa_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    en_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ar_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Page_Page_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Page",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nationality_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domains_Octa_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Octa_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Domains_Octa_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchoolType_Octa_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SchoolType_Octa_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SchoolType_Octa_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Octa",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_DeletedByUserId",
                table: "Domains",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_InsertedByUserId",
                table: "Domains",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Name",
                table: "Domains",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_UpdatedByUserId",
                table: "Domains",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nationality_CountryID",
                table: "Nationality",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Nationality_Name",
                table: "Nationality",
                column: "Name",
                unique: true);

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
                name: "IX_Octa_User_Name",
                table: "Octa",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_ar_name",
                table: "Page",
                column: "ar_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_en_name",
                table: "Page",
                column: "en_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_Page_ID",
                table: "Page",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_DeletedByUserId",
                table: "SchoolType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_InsertedByUserId",
                table: "SchoolType",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_Name",
                table: "SchoolType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_UpdatedByUserId",
                table: "SchoolType",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Octa");
        }
    }
}
