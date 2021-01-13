using System;
using System.Collections.Generic;
using System.Text;

namespace nkuotomasyon.entity
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string ClassRoomCode { get; set; }
        public string ClassRoomName { get; set; }
        public Faculty Faculty { get; set; }
    }
}
