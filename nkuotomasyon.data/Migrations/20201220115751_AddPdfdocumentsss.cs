using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class AddPdfdocumentsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode1",
                table: "PdfFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PdfFiles",
                table: "PdfFiles");

            migrationBuilder.DropIndex(
                name: "IX_PdfFiles_LessonCode1",
                table: "PdfFiles");

            migrationBuilder.DropColumn(
                name: "LessonCode1",
                table: "PdfFiles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PdfFiles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonCode",
                table: "PdfFiles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PdfFiles",
                table: "PdfFiles",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdfFiles_Lessons_LessonCode",
                table: "PdfFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PdfFiles",
                table: "PdfFiles");

            migrationBuilder.DropIndex(
                name: "IX_PdfFiles_LessonCode",
                table: "PdfFiles");

            migrationBuilder.AlterColumn<string>(
                name: "LessonCode",
                table: "PdfFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PdfFiles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "LessonCode1",
                table: "PdfFiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PdfFiles",
                table: "PdfFiles",
                column: "LessonCode");

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
    }
}
