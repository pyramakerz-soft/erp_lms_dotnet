using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Domain_Module_And_Its_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "Domain_Modules",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain_Id = table.Column<int>(type: "int", nullable: false),
                    Module_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain_Modules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domain_Modules_Domains_Domain_Id",
                        column: x => x.Domain_Id,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Domain_Modules_Modules_Module_Id",
                        column: x => x.Module_Id,
                        principalTable: "Modules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Modules_Domain_Id",
                table: "Domain_Modules",
                column: "Domain_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Modules_Module_Id",
                table: "Domain_Modules",
                column: "Module_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domain_Modules");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);
        }
    }
}
