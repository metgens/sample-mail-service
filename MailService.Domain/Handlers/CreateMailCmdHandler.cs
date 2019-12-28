using MailService.Common.Bus.Command;
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
    public class CreateMailCmdHandler : ICommandHandler<CreateMailCmd>
    {
        private readonly IMailWriteRepository _mailRepository;

        public CreateMailCmdHandler(IMailWriteRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task<Unit> Handle(CreateMailCmd request, CancellationToken cancellationToken)
        {
            var mail = Mail.Create(request.From, request.To, request.Subject, request.Body, request.IsHtml);

            await _mailRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}
