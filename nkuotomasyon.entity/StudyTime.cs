using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class StudyTime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudyProgram> StudyPrograms { get; set; }
    }
}
