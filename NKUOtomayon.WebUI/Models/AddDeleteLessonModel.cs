using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nkuotomasyon.entity;

namespace NKUOtomayon.WebUI.Models
{
    public class AddDeleteLessonModel
    {
        public AddDeleteLessonModel()
        {
            SelectedLesson = new List<Lesson>();
            NonSelectedLesson = new List<Lesson>();
            GradesFailedLessons = new List<Lesson>();
        }
       
        public List<Lesson> SelectedLesson { get; set; }
        public List<Lesson> NonSelectedLesson { get; set; }
        public Student Student { get; set; }
        public List<Lesson> GradesFailedLessons { get; set; }
    }
}
