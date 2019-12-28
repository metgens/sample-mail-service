using MailService.Common.Pagination;
using MailService.Domain;
using MailService.Domain.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MailService.Data.Repositories.Base;
using MailService.Contracts.Enums;
using System;
using System.Linq;

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
                .AsNoTracking()
                .GetPagedAsync(query);
        }

        public async Task<Mail> GetAsync(Guid id)
        {
            return await _context.Mails
                         .Include(x => x.Attachments)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MailStatus> GetStatusAsync(Guid id)
        {
            return await _context.Mails
                            .AsNoTracking()
                            .Where(x => x.Id == id)
                            .Select(x => x.Status)
                            .FirstOrDefaultAsync();
        }
    }
}
