using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
   public interface ITeacherRepository:IRepository<Teacher>
   {
       List<Lesson> GetTeacherLessonList (string teacherId);
       Teacher GeTeacherById(string id);
   }
}
