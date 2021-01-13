using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class StudentListByLessonModel
    {
        public StudentListByLessonModel()
        {
                Students=new List<Student>();
        }
        public List<Student> Students { get; set; }
        public string LessonCode { get; set; }
    }
}
