using Microsoft.EntityFrameworkCore;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
   public class NkuContext : DbContext
    {

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudyProgram> StudyPrograms { get; set; }
        public DbSet<StudyTime> StudyTimes { get; set; }
        public DbSet<StudyLesson> StudyLessons { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PdfFile> PdfFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=nkuDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyLesson>().HasKey(i => new
            {
                i.StudentId,
                i.LessonCode
            });
            modelBuilder.Entity<Lesson>().HasKey(i => new
            {
                i.LessonCode
            });
            
        }
    }
}