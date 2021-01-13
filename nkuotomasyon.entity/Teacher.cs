using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class Teacher
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<Lesson> Lessons { get; set; }
        public StudyProgram StudyProgram { get; set; }
    }
}
