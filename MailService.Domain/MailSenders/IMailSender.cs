using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Domain.MailSenders
{
    public interface IMailSender
    {
        Task SendMailAsync(Mail mail);
    }
}