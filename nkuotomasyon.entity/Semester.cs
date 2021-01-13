using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class Semester  //I must to look again, did not satisfy.Especially students part.
    
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Student> Students { get; set; }
    }
}
