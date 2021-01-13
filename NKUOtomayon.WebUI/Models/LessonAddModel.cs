using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class LessonAddModel
    {
        public LessonAddModel()
        {
            List<Teacher> teachers=new List<Teacher>();
            List<Semester> semesters=new List<Semester>();
            List<StudyProgram> studyPrograms=new List<StudyProgram>();
        }
        public string LessonCode { get; set; }
        public int Akts { get; set; }
        public int TheoryTime { get; set; }
        public int PracticeTime { get; set; }
        public string LessonName { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime StudyTime { get; set; }
        public string LessonDay { get; set; } 
        public int LessonStartHour { get; set; }
        public int LessonofNumber { get; set; }
        public Teacher Teacher { get; set; }
        public Semester Semester { get; set; }
        public StudyProgram StudyProgram { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Semester> Semesters { get; set; }
        public List<StudyProgram> StudyPrograms { get; set; }
    }
}
