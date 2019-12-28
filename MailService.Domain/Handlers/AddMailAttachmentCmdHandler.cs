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
    public class AddMailAttachmentCmdHandler : ICommandHandler<AddMailAttachmentCmd>
    {
        private readonly IMailWriteRepository _mailRepository;

        public AddMailAttachmentCmdHandler(IMailWriteRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }


        public async Task<Unit> Handle(AddMailAttachmentCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.MailId);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.MailId);

            mail.AddAttachment(request.Name, request.Content, request.Encoding, request.MediaType);

            await _mailRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
