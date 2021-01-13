using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class EditGradesFromTeacherModel
    {
        public string LessonName { get; set; }
        public int? MidTerm { get; set; }
        public int?  FinalTerm{ get; set; }
        public int? MakeUpExam { get; set; }
        public int? Average { get; set; }
        public string StudentId { get; set; }
        public string LessonCode { get; set; }
        public string LetterNote { get; set; }
        public bool FailedGrades { get; set; }
        public bool FailedAbsenteeism { get; set; }
    }
}
