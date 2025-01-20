using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class EditCreatePages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES
                (37, 'Employee Create', N'اضافة موظفين', 11, 0),
                (38, 'Employee Edit', N' نعديل بيانات الموظف', 11, 0),
                (39, 'Employee Details', N'بيانات الموظف', 11, 0),
                (40, 'Role Edit', N'تعديل الصلاحيات', 11, 0),
                (41, 'Role Create', N'اضافة صلاحيات', 11, 0),
                (42, 'SemesterView', N' بيانات الفصل الدراسي', 11, 0)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (37,38,39,40,41,42)");
        }
    }
}
