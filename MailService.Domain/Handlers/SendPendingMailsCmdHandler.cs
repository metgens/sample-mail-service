using MailService.Common.Bus.Command;
using MailService.Common.Bus.Event;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Contracts.Events;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MailService.Domain.RetryPolicy;
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
        private readonly IMailSenderFacade _mailSenderFacade;
        private readonly ILogger<SendPendingMailsCmdHandler> _logger;
        private readonly IEventBus _eventBus;
        private readonly IRetryPolicyFactory _retryPolicyFactory;

        public SendPendingMailsCmdHandler(IMailWriteRepository mailRepository, IMailSenderFacade mailSenderFacade,
            ILogger<SendPendingMailsCmdHandler> logger, IEventBus eventBus, IRetryPolicyFactory retryPolicyFactory)
        {
            _mailRepository = mailRepository;
            _mailSenderFacade = mailSenderFacade;
            _logger = logger;
            _eventBus = eventBus;
            _retryPolicyFactory = retryPolicyFactory;
        }

        public async Task<Unit> Handle(SendPendingMailsCmd request, CancellationToken cancellationToken)
        {
            var pendingMails = await _mailRepository.GetPendingAsync(request.MaxNumberToSend);

            _logger.LogDebug($"Preparing to send {pendingMails?.Count} mails");

            SendingMailsCompletedEvent eventCompleted = null;
            foreach (var pendingMail in pendingMails)
            {
                eventCompleted = await _mailSenderFacade.SendMailAsync(pendingMail);
                await _mailRepository.SaveChangesAsync();
            }

            await _eventBus.Publish(eventCompleted);

            return Unit.Value;
        }
    }
}
