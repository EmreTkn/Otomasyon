using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
 public   class EfCoreGradesRepository:EfCoreGenericRepository<Grade,NkuContext>,IGradesRepository
    {
        public Grade GetLessonByGradesId(int id)
        {
            using (var context=new NkuContext())
            {
                var gradeCard = context.Grades.Where(i => i.Id == id).Include(i => i.Lesson).FirstOrDefault();
                return gradeCard;

            }
        }

        public Grade GetGradesbyStudentAndLesson(string lessonCode, string studentId)
        {
            using (var context=new NkuContext())
            {
                var grade = context.Grades.Where(i => i.Student.Id == studentId && i.Lesson.LessonCode == lessonCode).Include(i=>i.Lesson).FirstOrDefault();
                return grade;
            }
        }

        public void CreateGrade(Grade grade,string studentId,string lessonCode)
        {
            using (var context=new NkuContext())
            {
                var student = context.Students.FirstOrDefault(i => i.Id == studentId);
                var lesson = context.Lessons.FirstOrDefault(i => i.LessonCode == lessonCode);
                grade.Lesson = lesson;
                grade.Student = student;
                context.Grades.Add(grade);
                context.SaveChangesAsync();
            }
        }
    }
}
