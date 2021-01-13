using System;
using System.Collections.Generic;
using System.Text;
using nkuotomasyon.entity;

namespace nkuotomasyon.data.Abstract
{
   public interface IPdfFileRepository:IRepository<PdfFile>
   {
       List<PdfFile> GetAllPdf(string lessonCode);
   }
}
