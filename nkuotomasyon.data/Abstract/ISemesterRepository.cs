using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
   public interface ISemesterRepository:IRepository<Semester>
   {
       void Update(string studentId, int semesterId);
   }
}
