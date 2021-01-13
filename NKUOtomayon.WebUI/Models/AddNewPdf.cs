
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class AddNewPdf
    {
        public AddNewPdf()
        {
            PdfFiles=new List<PdfFile>();
        }
        public string Name { get; set; }
        public string LessonCode { get; set; }
        public List<PdfFile> PdfFiles { get; set; }
    }
}
