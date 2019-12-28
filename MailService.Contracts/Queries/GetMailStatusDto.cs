using MailService.Common.Bus.Query;
using MailService.Common.Pagination;
using MailService.Contracts.DTOs;
using System;

namespace MailService.Contracts.Queries
{
    public class GetMailStatusQuery : IQuery<string>
    {
        public Guid MailId { get; }

        public GetMailStatusQuery(Guid mailId)
        {
            MailId = mailId;
        }
    }
}
