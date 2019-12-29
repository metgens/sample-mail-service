using MailService.Common.Bus.Command;
using MailService.Common.Bus.Event;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Contracts.Events;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class SendPendingMailsCmdHandler : ICommandHandler<SendPendingMailsCmd>
    {
        private readonly IMailWriteRepository _mailRepository;
        private readonly IMailSenderFactory _mailSenderFactory;
        private readonly ILogger<SendPendingMailsCmdHandler> _logger;
        private readonly IEventBus _eventBus;

        public SendPendingMailsCmdHandler(IMailWriteRepository mailRepository, IMailSenderFactory mailSenderFactory,
            ILogger<SendPendingMailsCmdHandler> logger, IEventBus eventBus)
        {
            _mailRepository = mailRepository;
            _mailSenderFactory = mailSenderFactory;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(SendPendingMailsCmd request, CancellationToken cancellationToken)
        {
            var pendingMails = await _mailRepository.GetPendingAsync(request.MaxNumberToSend);
            var mailSender = _mailSenderFactory.GetMailSender();

            _logger.LogDebug($"Preparing to send {pendingMails?.Count} mails");

            var exceptions = new Dictionary<Guid, string>();
            var sentMailsNo = 0;
            var rejectedMailsNo = 0;

            foreach (var pendingMail in pendingMails)
            {
                try
                {
                    await mailSender.SendMailAsync(pendingMail);
                    pendingMail.MarkAsSent();
                    sentMailsNo++;
                }
                catch (Exception ex)
                {
                    pendingMail.MarkAsRejected();
                    exceptions.Add(pendingMail.Id, ex.Message);
                    rejectedMailsNo++;
                    _logger.LogError($"Error sending mail with id: {pendingMail.Id}. Mail marked as 'rejected'.", ex);
                }

                await _mailRepository.SaveChangesAsync();
            }

            await _eventBus.Publish(new SendingMailsCompletedEvent(sentMailsNo, rejectedMailsNo, exceptions));

            return Unit.Value;
        }
    }
}
