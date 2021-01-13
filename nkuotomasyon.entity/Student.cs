using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class Student
    {
        public Student()
        {
            RegistrationTime=DateTime.Today;
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TcNumber { get; set; }
        public string SchoolNumber { get; set; }
        public StudyProgram StudyProgram { get; set; }
        public List<StudyLesson> StudyLessons { get; set; }
        public Faculty Faculty { get; set; }
        public DateTime RegistrationTime { get; set; }
        public Semester Semester { get; set; }
        public List<Grade> Grades { get; set; }
        public Photo Photo { get; set; }
       
    }
}
