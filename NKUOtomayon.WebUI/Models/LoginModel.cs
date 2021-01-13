using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKUOtomayon.WebUI.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string Tanım { get; set; }
        
    }
}
