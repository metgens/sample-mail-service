using MailService.Common.Bus.Query;
using MailService.Common.Pagination;
using MailService.Contracts.DTOs;
using System;

namespace MailService.Contracts.Queries
{
    public class GetMailQuery : IQuery<MailDto>
    {
        public Guid MailId { get; }

        public GetMailQuery(Guid mailId)
        {
            MailId = mailId;
        }
    }
}
