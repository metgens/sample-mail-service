using MailService.Common.Bus.Command;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class SendMailCmdHandler : ICommandHandler<SendMailCmd>
    {
        private readonly IMailWriteRepository _mailRepository;
        private readonly IMailSenderFactory _mailSenderFactory;

        public SendMailCmdHandler(IMailWriteRepository mailRepository, IMailSenderFactory mailSenderFactory)
        {
            _mailRepository = mailRepository;
            _mailSenderFactory = mailSenderFactory;
        }

        public async Task<Unit> Handle(SendMailCmd request, CancellationToken cancellationToken)
        {
            var mail = await _mailRepository.GetAsync(request.MailId);

            if (mail is null)
                throw AppException.NotExisting(typeof(Mail).Name, request.MailId);

            var mailSender = _mailSenderFactory.GetMailSender();
            await mailSender.SendMailAsync(mail);

            mail.MarkAsSent();
            await _mailRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}
