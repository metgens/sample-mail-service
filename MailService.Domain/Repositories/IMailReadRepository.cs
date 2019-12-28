using MailService.Common.Pagination;
using MailService.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Domain.Repositories
{
    public interface IMailReadRepository
    {
        Task<PagedResult<Mail>> GetAllAsync(PagedQuery query);
        Task<Mail> GetAsync(Guid id);
        Task<MailStatus> GetStatusAsync(Guid id);
    }
}
