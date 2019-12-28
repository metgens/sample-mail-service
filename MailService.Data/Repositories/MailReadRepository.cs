using MailService.Common.Pagination;
using MailService.Domain;
using MailService.Domain.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MailService.Data.Repositories.Base;

namespace MailService.Data.Repositories
{
    public class MailReadRepository : IMailReadRepository
    {
        private readonly MailServiceContext _context;

        public MailReadRepository(MailServiceContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Mail>> GetAllAsync(PagedQuery query)
        {
            return await _context.Mails
                .Include(x => x.Attachments)
                .GetPagedAsync<Mail>(query);
        }
    }
}
