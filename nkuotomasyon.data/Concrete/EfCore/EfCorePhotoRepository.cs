using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
   public class EfCorePhotoRepository:EfCoreGenericRepository<Photo,NkuContext>,IPhotoRepository
    {
        public Photo GetPhotoByUrl(string url)
        {
            using (var context=new NkuContext())
            {
                return context.Photos.FirstOrDefault(i => i.Url == url);
            }
        }

        public Photo GetPhotoByStudentId(string studentId)
        {
            using (var context = new NkuContext())
            {
                return context.Photos.FirstOrDefault(i => i.StudentId==studentId);
            }
        }
    }
}
