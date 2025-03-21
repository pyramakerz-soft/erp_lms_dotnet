﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Octa
{
    /// <inheritdoc />
    public partial class AccountingPageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page
                SET IsDisplay = 0
                WHERE ID = 50;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Page(ID, en_name, ar_name, Page_ID, IsDisplay) VALUES  
                (64, 'Accounting Tree', N'شجرة الحسابات', 48, 1);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Page
                SET IsDisplay = 1
                WHERE ID = 50;
            ");

            migrationBuilder.Sql("DELETE FROM Page WHERE ID IN (64)");
        }
    }
}
