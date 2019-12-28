﻿using MailService.Common.Bus.Command;
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
        private readonly IMailWriteRepository _mailRepository;

        public RemoveMailAttachmentCmdHandler(IMailWriteRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }


        public async Task<Unit> Handle(RemoveMailAttachmentCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.CommandId);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.CommandId);

            mail.RemoveAttachment(request.AttachmentId);

            await _mailRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
