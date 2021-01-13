using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
  public  interface IGradesRepository:IRepository<Grade>
  {
      Grade GetLessonByGradesId(int id);
      Grade GetGradesbyStudentAndLesson(string lessonCode, string studentId);
      void CreateGrade(Grade grade,string studentId,string lessonCode);
  }
}
