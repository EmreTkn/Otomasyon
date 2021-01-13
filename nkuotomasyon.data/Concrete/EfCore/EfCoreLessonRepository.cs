
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
    public class EfCoreLessonRepository : EfCoreGenericRepository<Lesson, NkuContext>, ILessonRepository
    {
        public Lesson GetByIdLesson(string id)
        {
            using (var context = new NkuContext())
            {
                return context.Lessons.Where(i => i.LessonCode == id).Include(i => i.Semester).FirstOrDefault();
            }
        }

        public async void DeleteFromStudent(string studentId, string lessonCode)
        {
            using (var context = new NkuContext())
            {
                var studentLesson =
                    context.StudyLessons.FirstOrDefault(i => i.LessonCode == lessonCode && i.StudentId == studentId);
                if (studentLesson != null)
                {
                    context.StudyLessons.Remove(studentLesson);
                    await context.SaveChangesAsync();
                }

            }
        }

        public void AddFromStudent(string lessonCode, string studentId)
        {
            if (studentId != null || lessonCode != null)
            {
                using (var context = new NkuContext())
                {
                    var student = context.Students.FirstOrDefault(i => i.Id == studentId);
                    var lesson = context.Lessons.FirstOrDefault(i => i.LessonCode == lessonCode);
                    var entity = new StudyLesson()
                    {
                        LessonCode = lessonCode, 
                        StudentId = studentId,
                        Lesson = lesson,
                        Student = student
                    };
                    context.StudyLessons.Add(entity);
                    context.SaveChangesAsync();
                }
            }
        }



        public void AddNewLesson(Lesson lesson, string teacherId, int semesterId, int studyProgramId)
        {
            using (var context = new NkuContext())
            {
                var teacher = context.Teachers.FirstOrDefault(i => i.Id == teacherId);
                var semester = context.Semesters.FirstOrDefault(i => i.Id == semesterId);
                var studyProgram = context.StudyPrograms.FirstOrDefault(i => i.Id == studyProgramId);

                lesson.Semester = semester;
                lesson.StudyProgram = studyProgram;
                lesson.Teacher = teacher;

                context.Lessons.Add(lesson);
                context.SaveChanges();

            }
        }

        public void UpdateToAddingLesson(string teacherId, int semesterId, int studyProgramId, string lessonCode)
        {
            using (var context = new NkuContext())
            {
                var lesson = context.Lessons.Where(i => i.LessonCode == lessonCode).FirstOrDefault();

                var teacher = context.Teachers.FirstOrDefault(i => i.Id == teacherId);
                var semester = context.Semesters.FirstOrDefault(i => i.Id == semesterId);
                var studyProgram = context.StudyPrograms.FirstOrDefault(i => i.Id == studyProgramId);

                lesson.Semester = semester;
                lesson.StudyProgram = studyProgram;
                lesson.Teacher = teacher;
                context.SaveChanges();

            }
        }

        public List<Lesson> GetLessonsWithSemester()
        {
            using (var context=new NkuContext())
            {
                var lessons = context.Lessons.Include(i => i.Semester);
                return lessons.ToList();
            }
        }

        public Lesson GetJustLesson(string lessonCode)
        {
            using (var context=new NkuContext())
            {
                return context.Lessons.FirstOrDefault(i => i.LessonCode == lessonCode);
            }
        }

        public List<Lesson> GetLessons()
        {
            using (var context=new NkuContext())
            {
                return context.Lessons.ToList();
            }
        }
    }
}
