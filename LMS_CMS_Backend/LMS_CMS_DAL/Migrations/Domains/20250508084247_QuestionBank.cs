using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_CMS_DAL.Migrations.Domains
{
    /// <inheritdoc />
    public partial class QuestionBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DragAndDropAnswer",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubBankQuestionID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragAndDropAnswer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBank",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<double>(type: "float", nullable: false),
                    EssayAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonID = table.Column<long>(type: "bigint", nullable: false),
                    BloomLevelID = table.Column<long>(type: "bigint", nullable: false),
                    DokLevelID = table.Column<long>(type: "bigint", nullable: false),
                    QuestionTypeID = table.Column<long>(type: "bigint", nullable: false),
                    CorrectAnswerID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionBank_BloomLevel_BloomLevelID",
                        column: x => x.BloomLevelID,
                        principalTable: "BloomLevel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBank_DokLevel_DokLevelID",
                        column: x => x.DokLevelID,
                        principalTable: "DokLevel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBank_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBank_QuestionType_QuestionTypeID",
                        column: x => x.QuestionTypeID,
                        principalTable: "QuestionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBankOption",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    QuestionBankID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBankOption", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionBankOption_QuestionBank_QuestionBankID",
                        column: x => x.QuestionBankID,
                        principalTable: "QuestionBank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBankTags",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionBankID = table.Column<long>(type: "bigint", nullable: false),
                    TagID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBankTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionBankTags_QuestionBank_QuestionBankID",
                        column: x => x.QuestionBankID,
                        principalTable: "QuestionBank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBankTags_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubBankQuestion",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionBankID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBankQuestion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubBankQuestion_QuestionBank_QuestionBankID",
                        column: x => x.QuestionBankID,
                        principalTable: "QuestionBank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropAnswer_SubBankQuestionID",
                table: "DragAndDropAnswer",
                column: "SubBankQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_BloomLevelID",
                table: "QuestionBank",
                column: "BloomLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_CorrectAnswerID",
                table: "QuestionBank",
                column: "CorrectAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_DokLevelID",
                table: "QuestionBank",
                column: "DokLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_LessonID",
                table: "QuestionBank",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBank_QuestionTypeID",
                table: "QuestionBank",
                column: "QuestionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankOption_QuestionBankID",
                table: "QuestionBankOption",
                column: "QuestionBankID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankTags_QuestionBankID",
                table: "QuestionBankTags",
                column: "QuestionBankID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBankTags_TagID",
                table: "QuestionBankTags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_SubBankQuestion_QuestionBankID",
                table: "SubBankQuestion",
                column: "QuestionBankID");

            migrationBuilder.AddForeignKey(
                name: "FK_DragAndDropAnswer_SubBankQuestion_SubBankQuestionID",
                table: "DragAndDropAnswer",
                column: "SubBankQuestionID",
                principalTable: "SubBankQuestion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionBank_QuestionBankOption_CorrectAnswerID",
                table: "QuestionBank",
                column: "CorrectAnswerID",
                principalTable: "QuestionBankOption",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionBank_QuestionBankOption_CorrectAnswerID",
                table: "QuestionBank");

            migrationBuilder.DropTable(
                name: "DragAndDropAnswer");

            migrationBuilder.DropTable(
                name: "QuestionBankTags");

            migrationBuilder.DropTable(
                name: "SubBankQuestion");

            migrationBuilder.DropTable(
                name: "QuestionBankOption");

            migrationBuilder.DropTable(
                name: "QuestionBank");
        }
    }
}
