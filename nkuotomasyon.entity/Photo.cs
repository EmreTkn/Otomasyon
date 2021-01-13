using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
  public class Photo
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public Student Student { get; set; }

    }
}
