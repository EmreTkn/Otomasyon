using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKUOtomayon.WebUI.Models
{
    public class UserDetailModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TcNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
