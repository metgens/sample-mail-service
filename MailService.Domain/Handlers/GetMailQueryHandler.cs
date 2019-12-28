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
    public class GetMailQueryHandler : IQueryHandler<GetMailQuery, MailDto>
    {
        private readonly IMailReadRepository _mailRepository;
        private readonly IMapper _mapper;

        public GetMailQueryHandler(IMailReadRepository mailRepository, IMapper mapper)
        {
            _mailRepository = mailRepository;
            _mapper = mapper;
        }

        public async Task<MailDto> Handle(GetMailQuery request, CancellationToken cancellationToken)
        {
            var results = await _mailRepository.GetAsync(request.MailId);
            return _mapper.Map<MailDto>(results);
        }
    }
}
