using Microsoft.AspNetCore.Identity;


namespace NKUOtomayon.WebUI.Identity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TcNumber { get; set; }
    }
}
