using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
   public interface IPhotoRepository:IRepository<Photo>
   {
       Photo GetPhotoByUrl(string url);
       Photo GetPhotoByStudentId(string studentId);
   }
}
