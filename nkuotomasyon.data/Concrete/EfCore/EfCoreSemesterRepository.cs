using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
    public class EfCoreSemesterRepository : EfCoreGenericRepository<Semester, NkuContext>, ISemesterRepository
    {
        public void Update(string studentId,int semesterId)
        {
            using (var context=new NkuContext())
            {
               var student= context.Students.Where(i => i.Id == studentId).Include(i => i.Semester).FirstOrDefault();
               var semester = context.Semesters.FirstOrDefault(i => i.Id == semesterId);
               if (student == null || semester == null) return;
               student.Semester = semester;
               context.Update(student);
               context.SaveChanges();
            }
        }
    }
}
