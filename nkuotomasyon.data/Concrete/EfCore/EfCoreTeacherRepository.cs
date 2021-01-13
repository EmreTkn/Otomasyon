using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
   public class EfCoreTeacherRepository:EfCoreGenericRepository<Teacher,NkuContext>,ITeacherRepository
    {
        public List<Lesson> GetTeacherLessonList(string teacherId)
        {
            using (var context=new NkuContext())
            {
                var lessons = context.Lessons.Where(i => i.Teacher.Id == teacherId).ToList();
                return lessons;
            }
        }

        public Teacher GeTeacherById(string id)
        {
            using (var context=new NkuContext())
            {
                return context.Teachers.Where(i => i.Id == id).Include(i => i.Lessons).FirstOrDefault();
            }
        }
    }
}
