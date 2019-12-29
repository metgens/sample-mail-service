using System.Threading.Tasks;
using MailService.Contracts.Events;

namespace MailService.Domain.MailSenders
{
    public interface IMailSenderFacade
    {
        Task<SendingMailsCompletedEvent> SendMailAsync(Mail mail);
    }
}