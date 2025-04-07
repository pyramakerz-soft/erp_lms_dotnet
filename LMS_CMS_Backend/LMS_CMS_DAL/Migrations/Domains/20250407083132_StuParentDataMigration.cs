using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class StuParentDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Parent_Id",
                table: "Student",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactMobile",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactRelation",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianRelation",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegisteredInNoor",
                table: "Student",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherEmail",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherExperiences",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNationalID",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNationalIDExpiredDate",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherPassportExpireDate",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherPassportNo",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherProfession",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherQualification",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherWorkPlace",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIDExpiredDate",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportExpiredDate",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpContactMobile",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpContactName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpContactRelation",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousSchool",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalID",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIDExpiredDate",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNoExpiredDate",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkPlace",
                table: "Parent",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EmergencyContactMobile",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EmergencyContactName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EmergencyContactRelation",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GuardianRelation",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "IsRegisteredInNoor",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherEmail",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherExperiences",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherNationalID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherNationalIDExpiredDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherPassportExpireDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherPassportNo",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherProfession",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherQualification",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MotherWorkPlace",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NationalIDExpiredDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PassportExpiredDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PickUpContactMobile",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PickUpContactName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PickUpContactRelation",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PreviousSchool",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NationalID",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "NationalIDExpiredDate",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "PassportNoExpiredDate",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Parent");

            migrationBuilder.DropColumn(
                name: "WorkPlace",
                table: "Parent");

            migrationBuilder.AlterColumn<long>(
                name: "Parent_Id",
                table: "Student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
