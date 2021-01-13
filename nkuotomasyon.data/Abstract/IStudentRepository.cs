using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
   public interface IStudentRepository:IRepository<Student>
   {
       List<Lesson> GetStudentLessons(string id);
       Teacher GetTeacherForLesson(string lessonCode);
       Semester GetSemesterForStudent(string id);
       StudyProgram GetStudyProgramForStudent(string id);
       List<Grade> GetStudentGrades(string id);
       Student GetStudentById(string id);
       void Update(string studentId, string[] lessonCode);
       List<StudyLesson> GetStudentsByLesson(string lessonCode);
       Student GetJustStudent(string id);
   }
}
