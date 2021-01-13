using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class AddingClassRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamClassRoomId",
                table: "Lessons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalExamTime",
                table: "Lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LessonClassRoomId",
                table: "Lessons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MakeUpExamTime",
                table: "Lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MidTermTime",
                table: "Lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ClassRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassRoomCode = table.Column<string>(type: "TEXT", nullable: true),
                    ClassRoomName = table.Column<string>(type: "TEXT", nullable: true),
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassRoom_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ExamClassRoomId",
                table: "Lessons",
                column: "ExamClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonClassRoomId",
                table: "Lessons",
                column: "LessonClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoom_FacultyId",
                table: "ClassRoom",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_StudentId",
                table: "Photo",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ClassRoom_ExamClassRoomId",
                table: "Lessons",
                column: "ExamClassRoomId",
                principalTable: "ClassRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ClassRoom_LessonClassRoomId",
                table: "Lessons",
                column: "LessonClassRoomId",
                principalTable: "ClassRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRoom_ExamClassRoomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRoom_LessonClassRoomId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "ClassRoom");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ExamClassRoomId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonClassRoomId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ExamClassRoomId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "FinalExamTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonClassRoomId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "MakeUpExamTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "MidTermTime",
                table: "Lessons");
        }
    }
}
