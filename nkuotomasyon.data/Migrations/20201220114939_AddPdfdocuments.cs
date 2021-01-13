using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class AddPdfdocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdfFiles",
                columns: table => new
                {
                    LessonCode = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LessonCode1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfFiles", x => x.LessonCode);
                    table.ForeignKey(
                        name: "FK_PdfFiles_Lessons_LessonCode1",
                        column: x => x.LessonCode1,
                        principalTable: "Lessons",
                        principalColumn: "LessonCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PdfFiles_LessonCode1",
                table: "PdfFiles",
                column: "LessonCode1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdfFiles");
        }
    }
}
