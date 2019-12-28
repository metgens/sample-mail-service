using MailService.Common.Bus.Command;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class SendPendingMailsCmdHandler : ICommandHandler<SendPendingMailsCmd>
    {
        private readonly IMailWriteRepository _mailRepository;
        private readonly IMailSenderFactory _mailSenderFactory;
        private readonly ILogger<SendPendingMailsCmdHandler> _logger;

        public SendPendingMailsCmdHandler(IMailWriteRepository mailRepository, IMailSenderFactory mailSenderFactory,
            ILogger<SendPendingMailsCmdHandler> logger)
        {
            _mailRepository = mailRepository;
            _mailSenderFactory = mailSenderFactory;
            _logger = logger;
        }

        public async Task<Unit> Handle(SendPendingMailsCmd request, CancellationToken cancellationToken)
        {
            var pendingMails = await _mailRepository.GetPendingAsync(request.MaxNumberToSend);
            var mailSender = _mailSenderFactory.GetMailSender();

            foreach (var pendingMail in pendingMails)
            {
                try
                {
                    await mailSender.SendMailAsync(pendingMail);
                    pendingMail.MarkAsSent();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError($"Error sending mail with id: {pendingMail.Id}. Mail marked as 'rejected'.", ex);
                    pendingMail.MarkAsRejected();
                }
            }
                       
            await _mailRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}
