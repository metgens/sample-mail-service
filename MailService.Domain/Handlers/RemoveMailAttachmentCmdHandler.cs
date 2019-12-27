using MailService.Common.Bus.Command;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class RemoveMailAttachmentCmdHandler : ICommandHandler<RemoveMailAttachmentCmd>
    {
        private readonly IMailRepository _mailRepository;

        public RemoveMailAttachmentCmdHandler(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }


        public async Task<Unit> Handle(RemoveMailAttachmentCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.Id);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.Id);

            mail.RemoveAttachment(request.AttachmentId);

            await _mailRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
