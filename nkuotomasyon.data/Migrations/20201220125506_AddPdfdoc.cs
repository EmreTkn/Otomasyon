using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class AddPdfdoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode",
                table: "PdfFiles");

            migrationBuilder.DropIndex(
                name: "IX_PdfFiles_LessonCode",
                table: "PdfFiles");

            migrationBuilder.AddColumn<string>(
                name: "LessonCode1",
                table: "PdfFiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "PdfFiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdfFiles_LessonCode1",
                table: "PdfFiles",
                column: "LessonCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode1",
                table: "PdfFiles",
                column: "LessonCode1",
                principalTable: "Lessons",
                principalColumn: "LessonCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode1",
                table: "PdfFiles");

            migrationBuilder.DropIndex(
                name: "IX_PdfFiles_LessonCode1",
                table: "PdfFiles");

            migrationBuilder.DropColumn(
                name: "LessonCode1",
                table: "PdfFiles");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "PdfFiles");

            migrationBuilder.CreateIndex(
                name: "IX_PdfFiles_LessonCode",
                table: "PdfFiles",
                column: "LessonCode");

            migrationBuilder.AddForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode",
                table: "PdfFiles",
                column: "LessonCode",
                principalTable: "Lessons",
                principalColumn: "LessonCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
