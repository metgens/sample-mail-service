using AutoMapper;
using MailService.Common.Bus.Query;
using MailService.Common.Pagination;
using MailService.Contracts.DTOs;
using MailService.Contracts.Queries;
using MailService.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Domain.Handlers
{
    public class GetMailStatusQueryHandler : IQueryHandler<GetMailStatusQuery, string>
    {
        private readonly IMailReadRepository _mailRepository;

        public GetMailStatusQueryHandler(IMailReadRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task<string> Handle(GetMailStatusQuery request, CancellationToken cancellationToken)
        {
            var results = await _mailRepository.GetStatusAsync(request.MailId);
            return results.ToString();
        }
    }
}
