using MailService.Common.Bus.Command;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class EditMailCmdHandler : ICommandHandler<EditMailCmd>
    {
        private readonly IMailWriteRepository _mailRepository;

        public EditMailCmdHandler(IMailWriteRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task<Unit> Handle(EditMailCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.Id);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.Id);

            mail.Edit(request.From, request.To, request.Subject, request.Body, request.IsBodyHtml, request.Priority);

            await _mailRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
