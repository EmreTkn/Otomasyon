using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKUOtomayon.WebUI.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TcNumber { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
