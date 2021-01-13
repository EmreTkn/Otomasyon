using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class Faculty
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public List<StudyProgram> StudyPrograms { get; set; }
        public List<Student> Students { get; set; }
        public List<ClassRoom> ClassRooms { get; set; }
    }
}
