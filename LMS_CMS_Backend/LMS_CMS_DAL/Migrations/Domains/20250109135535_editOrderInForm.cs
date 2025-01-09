using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class editOrderInForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterationFormInterview_InterviewState_InterviewStateID",
                table: "RegisterationFormInterview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewState",
                table: "InterviewState");

            migrationBuilder.RenameTable(
                name: "InterviewState",
                newName: "InterViewState");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewState_Name",
                table: "InterViewState",
                newName: "IX_InterViewState_Name");

            migrationBuilder.AlterColumn<int>(
                name: "OrderInForm",
                table: "RegistrationCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterViewState",
                table: "InterViewState",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterationFormInterview_InterViewState_InterviewStateID",
                table: "RegisterationFormInterview",
                column: "InterviewStateID",
                principalTable: "InterViewState",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterationFormInterview_InterViewState_InterviewStateID",
                table: "RegisterationFormInterview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterViewState",
                table: "InterViewState");

            migrationBuilder.RenameTable(
                name: "InterViewState",
                newName: "InterviewState");

            migrationBuilder.RenameIndex(
                name: "IX_InterViewState_Name",
                table: "InterviewState",
                newName: "IX_InterviewState_Name");

            migrationBuilder.AlterColumn<string>(
                name: "OrderInForm",
                table: "RegistrationCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewState",
                table: "InterviewState",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterationFormInterview_InterviewState_InterviewStateID",
                table: "RegisterationFormInterview",
                column: "InterviewStateID",
                principalTable: "InterviewState",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
