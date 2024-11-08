﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pages_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pyramakerz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pyramakerz", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roles_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Domain_Page_Details",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain_Page_Details", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Domain_Page_Details_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Parent_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Parents_Parent_Id",
                        column: x => x.Parent_Id,
                        principalTable: "Parents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_ID = table.Column<long>(type: "bigint", nullable: false),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Domains_Domain_ID",
                        column: x => x.Domain_ID,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role_Detailes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allow_Edit = table.Column<bool>(type: "bit", nullable: false),
                    Allow_Delete = table.Column<bool>(type: "bit", nullable: false),
                    Role_ID = table.Column<long>(type: "bigint", nullable: false),
                    Page_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Detailes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Pages_Page_ID",
                        column: x => x.Page_ID,
                        principalTable: "Pages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_Detailes_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Domain_id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Domains_Domain_id",
                        column: x => x.Domain_id,
                        principalTable: "Domains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_Employees_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Schools_Employees_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Schools_Employees_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Domain_ID",
                table: "Domain_Page_Details",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domain_Page_Details_Page_ID",
                table: "Domain_Page_Details",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Name",
                table: "Domains",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Domain_ID",
                table: "Employees",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Role_ID",
                table: "Employees",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_User_Name",
                table: "Employees",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Name",
                table: "Pages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Page_ID",
                table: "Pages",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Email",
                table: "Parents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_User_Name",
                table: "Parents",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_Email",
                table: "Pyramakerz",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pyramakerz_User_Name",
                table: "Pyramakerz",
                column: "User_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_Page_ID",
                table: "Role_Detailes",
                column: "Page_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Detailes_Role_ID",
                table: "Role_Detailes",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Domain_ID",
                table: "Roles",
                column: "Domain_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_DeletedByUserId",
                table: "Schools",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_Domain_id",
                table: "Schools",
                column: "Domain_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_InsertedByUserId",
                table: "Schools",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UpdatedByUserId",
                table: "Schools",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Parent_Id",
                table: "Students",
                column: "Parent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_User_Name",
                table: "Students",
                column: "User_Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domain_Page_Details");

            migrationBuilder.DropTable(
                name: "Pyramakerz");

            migrationBuilder.DropTable(
                name: "Role_Detailes");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
