using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Domain.MailSenders
{
    public interface IMailSender
    {
        Task SendMailAsync(string from, List<string> to, string subject, string body, bool isBodyHtml);
    }
}