using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class AddingClassRoo2m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoom_Faculties_FacultyId",
                table: "ClassRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRoom_ExamClassRoomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRoom_LessonClassRoomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Students_StudentId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoom",
                table: "ClassRoom");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameTable(
                name: "ClassRoom",
                newName: "ClassRooms");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_StudentId",
                table: "Photos",
                newName: "IX_Photos_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoom_FacultyId",
                table: "ClassRooms",
                newName: "IX_ClassRooms_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRooms_Faculties_FacultyId",
                table: "ClassRooms",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ClassRooms_ExamClassRoomId",
                table: "Lessons",
                column: "ExamClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_ClassRooms_LessonClassRoomId",
                table: "Lessons",
                column: "LessonClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Students_StudentId",
                table: "Photos",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRooms_Faculties_FacultyId",
                table: "ClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRooms_ExamClassRoomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_ClassRooms_LessonClassRoomId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Students_StudentId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameTable(
                name: "ClassRooms",
                newName: "ClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_StudentId",
                table: "Photo",
                newName: "IX_Photo_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRooms_FacultyId",
                table: "ClassRoom",
                newName: "IX_ClassRoom_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoom",
                table: "ClassRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoom_Faculties_FacultyId",
                table: "ClassRoom",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Students_StudentId",
                table: "Photo",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
