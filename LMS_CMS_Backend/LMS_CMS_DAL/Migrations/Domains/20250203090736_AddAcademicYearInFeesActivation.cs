using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class AddAcademicYearInFeesActivation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FeeDiscountTypeID",
                table: "FeesActivation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AcademicYearId",
                table: "FeesActivation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeesActivation_AcademicYearId",
                table: "FeesActivation",
                column: "AcademicYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeesActivation_AcademicYear_AcademicYearId",
                table: "FeesActivation",
                column: "AcademicYearId",
                principalTable: "AcademicYear",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeesActivation_AcademicYear_AcademicYearId",
                table: "FeesActivation");

            migrationBuilder.DropIndex(
                name: "IX_FeesActivation_AcademicYearId",
                table: "FeesActivation");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "FeesActivation");

            migrationBuilder.AlterColumn<long>(
                name: "FeeDiscountTypeID",
                table: "FeesActivation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
