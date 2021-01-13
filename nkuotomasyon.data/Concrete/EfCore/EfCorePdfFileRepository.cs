using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using nkuotomasyon.data.Abstract;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Concrete.EfCore
{
  public  class EfCorePdfFileRepository:EfCoreGenericRepository<PdfFile,NkuContext>,IPdfFileRepository
    {
        public List<PdfFile> GetAllPdf(string lessonCode)
        {
            using (var context=new NkuContext())
            {
                var pdfList = context.PdfFiles.Where(i => i.LessonCode == lessonCode);
                return pdfList.ToList();
            }
        }
    }
}
