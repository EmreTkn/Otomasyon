using System.Threading.Tasks;

namespace NKUOtomayon.WebUI.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

}
