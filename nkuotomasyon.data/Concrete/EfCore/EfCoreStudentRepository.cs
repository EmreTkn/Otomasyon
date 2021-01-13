using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
    public class EfCoreStudentRepository : EfCoreGenericRepository<Student, NkuContext>, IStudentRepository
    {
        public List<Lesson> GetStudentLessons(string id)
        {
            using (var context = new NkuContext())
            {
                var lessons = context.Lessons
                    .Include(i => i.StudyLessons)
                    .ThenInclude(i => i.Student)
                   .Where(i => i.StudyLessons.Any(a => a.StudentId == id));
                return lessons.ToList();
            
            }
        }

        public Teacher GetTeacherForLesson(string lessonCode)
        {
            using (var context=new NkuContext())
            {
                var teacherLesson = context.Lessons
                    .Where(i => i.LessonCode == lessonCode)
                    .Include(i => i.Teacher).FirstOrDefault();

                if (teacherLesson != null) return teacherLesson.Teacher;
            }

            return null;
        }

        public Semester GetSemesterForStudent(string id)
        {
            using (var context=new NkuContext())
            {
                var studentSemesters = context.Students
                    .Where(i => i.Id == id)
                    .Include(i => i.Semester)
                    .FirstOrDefault();
                if (studentSemesters != null) return studentSemesters.Semester;
            }

            return null;
        }

        public StudyProgram GetStudyProgramForStudent(string id)
        {
            using (var context=new NkuContext())
            {
                var studentProgram = context.Students
                    .Where(i => i.Id == id)
                    .Include(i => i.StudyProgram)
                    .FirstOrDefault();
                if (studentProgram != null) return studentProgram.StudyProgram;
            }

            return null;
        }

        public List<Grade> GetStudentGrades(string id)
        {
            using (var context=new NkuContext())
            {
                var grades = context.Grades
                    .Include(i=>i.Lesson).ThenInclude(i=>i.Semester).Where(i => i.Student.Id == id);
                   
                return grades.ToList();
            }
        }

        public Student GetStudentById(string id)
        {
            using (var context=new NkuContext())
            {
                return context.Students.Where(i => i.Id == id).Include(i => i.Semester).FirstOrDefault();
            }
        }

        public void Update(string studentId, string[] lessonCode)
        {
            using (var context=new NkuContext())
            {
                var studentCurrent = context.Students
                    .Include(i => i.StudyLessons)
                    .FirstOrDefault(i => i.Id == studentId);
                if (studentCurrent!=null)
                {
                    studentCurrent.StudyLessons = lessonCode.Select(stid => new StudyLesson()
                    {
                        LessonCode =stid ,
                        StudentId = studentCurrent.Id
                    }).ToList();
                    context.SaveChanges();
                }
            }
        }

        public List<StudyLesson> GetStudentsByLesson(string lessonCode)
        {
            using (var context=new NkuContext())
            {
                var studyLessons = context.StudyLessons.Where(i => i.LessonCode == lessonCode).Include(i=>i.Student).ToList();
                return studyLessons;
            }
        }

        public Student GetJustStudent(string id)
        {
            using (var context =new NkuContext())
            {
                return context.Students.FirstOrDefault(i => i.Id == id);
            }
        }
    }
}
