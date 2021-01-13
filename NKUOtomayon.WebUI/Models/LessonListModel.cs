using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class  LessonListModel
    {
        public List<Lesson> Lesson { get; set; }

        public List<Grade> Grades { get; set; }
        //public int Avarege { get; set; }
        //public string Letter { get; set; }
        //public string SuccessStatus { get; set; }
        //public string Absenteesm { get; set; }
    }
}
