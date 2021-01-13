using Microsoft.EntityFrameworkCore.Migrations;

namespace nkuotomasyon.data.Migrations
{
    public partial class School1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LessonDay",
                table: "Lessons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonStartHour",
                table: "Lessons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonofNumber",
                table: "Lessons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonDay",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonStartHour",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonofNumber",
                table: "Lessons");
        }
    }
}
