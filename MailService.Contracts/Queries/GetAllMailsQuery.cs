using MailService.Common.Bus.Query;
using MailService.Common.Pagination;
using MailService.Contracts.DTOs;

namespace MailService.Contracts.Queries
{
    public class GetAllMailsQuery : PagedQuery, IQuery<PagedResult<MailDto>>
    {
    }
}
