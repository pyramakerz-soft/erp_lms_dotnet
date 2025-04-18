﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class InsertRegistrationTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO QuestionType(ID, Name) VALUES
                (1, 'True/False'),
                (2, 'MCQ'),
                (3, 'Essay');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO FieldType(ID, Name) VALUES
                (1, 'Text One Line'),
                (2, 'Text Multi Lines'),
                (3, 'Date'),
                (4, 'Multi Checkboxes'),
                (5, 'Multi Options'),
                (6, 'Attachment'),
                (7, 'Dropdown');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO RegisterationFormState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined'),
                (4, 'Waiting List');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO TestState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined'),
                (4, 'Done');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO InterviewState(ID, Name) VALUES
                (1, 'Pending'),
                (2, 'Accepted'),
                (3, 'Declined');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO RegistrationCategory(EnName, ArName, OrderInForm) VALUES
                  ('Personal Data Of Student', N'البيانات الشخصية للطالب', 1),
                  ('Guardian’s Information', N'معلومات الوصي', 2),
                  ('Mother’s Information', N'معلومات عن الأم', 3);
            ");

            migrationBuilder.Sql(@"
                INSERT INTO CategoryField(EnName, ArName, OrderInForm, IsMandatory, RegistrationCategoryID, FieldTypeID) VALUES
                  ('Student Full Name English', N'الاسم الكامل للطالب باللغة الإنجليزية', 1, 'true', 1, 1),
                  ('Student Full Name Arabic', N'الاسم الكامل للطالب باللغة العربية', 2, 'true', 1, 1),
                  ('Gender', N'الجنس', 3, 'true', 1, 7),
                  ('Date Of Birth', N'تاريخ الميلاد', 4, 'true', 1, 3),
                  ('Nationality', N'الجنسية', 5, 'true', 1, 7),
                  ('Religion', N'الديانة', 6, 'true', 1, 7),
                  ('School', N'المدرسة', 7, 'true', 1, 7),
                  ('Academic Year', N'السنة الدراسية', 8, 'true', 1, 7),
                  ('Current Grade', N'الصف الحالي', 9, 'true', 1, 7),
                  ('Transferred Grad', N'الصف المنقول منه', 10, 'true', 1, 1),
                  ('Student’s ID No.', N'رقم هوية الطالب', 11, 'true', 1, 1),
                  ('Student’s Passport No.', N'رقم جواز سفر الطالب', 12, 'false', 1, 1),
                  ('Previous School', N'المدرسة السابقة', 13, 'false', 1, 1),
                  ('Do You Wish to Use School Transportation System?', N'هل ترغب في استخدام نظام النقل المدرسي؟', 14, 'true', 1, 7),
                  ('Guardian’s Name', N'اسم الوصي', 1, 'true', 2, 1),
                  ('Passport No.', N'رقم جواز السفر', 2, 'false', 2, 1),
                  ('ID No.', N'رقم الهوية', 3, 'true', 2, 1),
                  ('Qualification', N'المؤهلات', 4, 'false', 2, 2),
                  ('Place of work', N'مكان العمل', 5, 'false', 2, 1),
                  ('Mobile No.', N'رقم الهاتف', 6, 'true', 2, 1),
                  ('E-mail Address', N'عنوان البريد الإلكتروني', 7, 'true', 2, 1),
                  ('Guardian’s Name', N'اسم الوصي', 1, 'true', 3, 1),
                  ('Passport No.', N'رقم جواز السفر', 2, 'false', 3, 1),
                  ('ID No.', N'رقم الهوية', 3, 'true', 3, 1),
                  ('Qualification', N'المؤهلات', 4, 'false', 3, 2),
                  ('Place of work', N'مكان العمل', 5, 'false', 3, 1),
                  ('Mobile No.', N'رقم الهاتف', 6, 'true', 3, 1),
                  ('E-mail Address', N'عنوان البريد الإلكتروني', 7, 'true', 3, 1);
            ");

            migrationBuilder.Sql(@"
                INSERT INTO RegistrationForm(Name) VALUES
                  ('Registration Form');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO RegistrationFormCategory(RegistrationFormID, RegistrationCategoryID) VALUES
                  (1,1),
                  (1,2),
                  (1,3);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM QuestionType WHERE ID IN (1,2,3)");
            migrationBuilder.Sql("DELETE FROM FieldType WHERE ID IN (1,2,3,4,5,6,7)");
            migrationBuilder.Sql("DELETE FROM RegisterationFormState WHERE ID IN (1,2,3,4)");
            migrationBuilder.Sql("DELETE FROM InterviewState WHERE ID IN (1,2,3)");
            migrationBuilder.Sql("DELETE FROM TestState WHERE ID IN (1,2,3,4)");
            migrationBuilder.Sql("DELETE FROM RegistrationCategory WHERE ID IN (1,2,3)");
            migrationBuilder.Sql("DELETE FROM CategoryField WHERE ID IN (1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28)");
            migrationBuilder.Sql("DELETE FROM RegistrationForm WHERE ID IN (1)");
            migrationBuilder.Sql("DELETE FROM RegistrationFormCategory WHERE ID IN (1,2,3)");
        }
    }
}
