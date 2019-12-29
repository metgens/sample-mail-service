using MailService.Common.Bus.Command;
using MailService.Common.Bus.Event;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Contracts.Events;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MailService.Domain.RetryPolicy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class SendMailCmdHandler : ICommandHandler<SendMailCmd>
    {
        private readonly IMailWriteRepository _mailRepository;
        private readonly IMailSenderFacade _mailSenderFacade;
        private readonly IEventBus _eventBus;
        private readonly IRetryPolicyFactory _retryPolicyFactory;

        public SendMailCmdHandler(IMailWriteRepository mailRepository, IMailSenderFacade mailSenderFacade,
            IEventBus eventBus)
        {
            _mailRepository = mailRepository;
            _mailSenderFacade = mailSenderFacade;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(SendMailCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.MailId);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.MailId);

            var eventCompleted = await _mailSenderFacade.SendMailAsync(mail);
            await _mailRepository.SaveChangesAsync();

            await _eventBus.Publish(eventCompleted);

            return Unit.Value;
        }
    }
}
