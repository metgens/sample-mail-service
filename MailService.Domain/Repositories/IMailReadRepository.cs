using MailService.Common.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Domain.Repositories
{
    public interface IMailReadRepository
    {
        Task<PagedResult<Mail>> GetAllAsync(PagedQuery query);
    }
}
