using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class UpdateFollowUpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    ClassroomId = table.Column<long>(type: "bigint", nullable: false),
                    Complains = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisId = table.Column<long>(type: "bigint", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendSMS = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUps_Classroom_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classroom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUps_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowUpDrugs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowUpId = table.Column<long>(type: "bigint", nullable: false),
                    DrugId = table.Column<long>(type: "bigint", nullable: false),
                    DoseId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FollowUpDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Dose_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Dose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_InsertedByUserId",
                        column: x => x.InsertedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_Employee_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Employee",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FollowUpDrugs_FollowUps_FollowUpId",
                        column: x => x.FollowUpId,
                        principalTable: "FollowUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DeletedByUserId",
                table: "FollowUpDrugs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DoseId",
                table: "FollowUpDrugs",
                column: "DoseId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_DrugId",
                table: "FollowUpDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_FollowUpId",
                table: "FollowUpDrugs",
                column: "FollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_InsertedByUserId",
                table: "FollowUpDrugs",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpDrugs_UpdatedByUserId",
                table: "FollowUpDrugs",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_ClassroomId",
                table: "FollowUps",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_DeletedByUserId",
                table: "FollowUps",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_DiagnosisId",
                table: "FollowUps",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_InsertedByUserId",
                table: "FollowUps",
                column: "InsertedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_SchoolId",
                table: "FollowUps",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_UpdatedByUserId",
                table: "FollowUps",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUpDrugs");

            migrationBuilder.DropTable(
                name: "FollowUps");
        }
    }
}
