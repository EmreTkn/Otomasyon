﻿using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
   public class StudyLesson
    {
        public string LessonCode { get; set; }
        public Lesson Lesson { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
