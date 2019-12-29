using MailService.Contracts.Events;
using MailService.Domain.RetryPolicy;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Domain.MailSenders
{
    public class MailSenderFacade : IMailSenderFacade
    {
        private IMailSenderFactory _mailSenderFactory;
        private IMailSender _mailSender;
        private IRetryPolicyFactory _retryPolicyFactory;
        private readonly ILogger<MailSenderFacade> _logger;
        private IAsyncPolicy _retry;
        private SendingMailsCompletedEvent _event;

        public MailSenderFacade(IMailSenderFactory mailSenderFactory, IRetryPolicyFactory retryPolicyFactory,
            ILogger<MailSenderFacade> logger)
        {
            _mailSenderFactory = mailSenderFactory;
            _mailSender = _mailSenderFactory.GetMailSender();
            _retryPolicyFactory = retryPolicyFactory;
            _logger = logger;
            _retry = _retryPolicyFactory.GetForSendingMails();

            _event = new SendingMailsCompletedEvent();
        }

        public async Task<SendingMailsCompletedEvent> SendMailAsync(Mail mail)
        {
            try
            {
                await _retry.ExecuteAsync(() => _mailSender.SendMailAsync(mail));
                mail.MarkAsSent();
                _event.ReportSucceded();
            }
            catch (Exception ex)
            {
                mail.MarkAsRejected();
                _event.ReportRejected(mail.Id, ex.Message);
                _logger.LogError($"Error sending mail with id: {mail.Id}. Mail marked as 'rejected'.", ex);
            }

            return _event;
        }
    }
}
