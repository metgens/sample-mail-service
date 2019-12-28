namespace MailService.Domain.MailSenders
{
    public interface IMailSenderFactory
    {
        IMailSender GetMailSender();
    }
}