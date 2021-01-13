using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class School : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FacultyName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SemesterName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramName = table.Column<string>(type: "TEXT", nullable: true),
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudyTimeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPrograms_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyPrograms_StudyTimes_StudyTimeId",
                        column: x => x.StudyTimeId,
                        principalTable: "StudyTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    TcNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SchoolNumber = table.Column<string>(type: "TEXT", nullable: true),
                    StudyProgramId = table.Column<int>(type: "INTEGER", nullable: true),
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: true),
                    RegistrationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SemesterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_StudyPrograms_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "StudyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyProgramId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_StudyPrograms_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "StudyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonCode = table.Column<string>(type: "TEXT", nullable: false),
                    Akts = table.Column<int>(type: "INTEGER", nullable: false),
                    TheoryTime = table.Column<int>(type: "INTEGER", nullable: false),
                    PracticeTime = table.Column<int>(type: "INTEGER", nullable: false),
                    LessonName = table.Column<string>(type: "TEXT", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StudyTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: true),
                    SemesterId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudyProgramId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonCode);
                    table.ForeignKey(
                        name: "FK_Lessons_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_StudyPrograms_StudyProgramId",
                        column: x => x.StudyProgramId,
                        principalTable: "StudyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LessonCode = table.Column<string>(type: "TEXT", nullable: true),
                    StudentId = table.Column<string>(type: "TEXT", nullable: true),
                    FailedAbsenteeism = table.Column<bool>(type: "INTEGER", nullable: false),
                    FailedLowGrade = table.Column<bool>(type: "INTEGER", nullable: false),
                    MidTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalExam = table.Column<int>(type: "INTEGER", nullable: false),
                    MakeUpExam = table.Column<int>(type: "INTEGER", nullable: false),
                    Average = table.Column<int>(type: "INTEGER", nullable: false),
                    GradeLetter = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Lessons_LessonCode",
                        column: x => x.LessonCode,
                        principalTable: "Lessons",
                        principalColumn: "LessonCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyLessons",
                columns: table => new
                {
                    LessonCode = table.Column<string>(type: "TEXT", nullable: false),
                    StudentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLessons", x => new { x.StudentId, x.LessonCode });
                    table.ForeignKey(
                        name: "FK_StudyLessons_Lessons_LessonCode",
                        column: x => x.LessonCode,
                        principalTable: "Lessons",
                        principalColumn: "LessonCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyLessons_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LessonCode",
                table: "Grades",
                column: "LessonCode");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SemesterId",
                table: "Lessons",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StudyProgramId",
                table: "Lessons",
                column: "StudyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeacherId",
                table: "Lessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudyProgramId",
                table: "Students",
                column: "StudyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLessons_LessonCode",
                table: "StudyLessons",
                column: "LessonCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_FacultyId",
                table: "StudyPrograms",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_StudyTimeId",
                table: "StudyPrograms",
                column: "StudyTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StudyProgramId",
                table: "Teachers",
                column: "StudyProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "StudyLessons");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "StudyPrograms");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "StudyTimes");
        }
    }
}
