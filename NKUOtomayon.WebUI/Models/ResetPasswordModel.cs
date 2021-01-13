using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKUOtomayon.WebUI.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
