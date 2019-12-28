using MailService.Data.Repositories.Base;
using MailService.Domain;
using MailService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Data.Repositories
{
    public class MailWriteRepository : IMailWriteRepository
    {
        private readonly IRepository<Mail> _repository;

        public MailWriteRepository(IRepository<Mail> repository)
        {
            _repository = repository;
        }

        public void Add(Mail mail)
        {
            _repository.Add(mail);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public async Task<Mail> GetAsync(Guid id)
        {
            return await _repository.Context.Mails
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Mail>> GetPendingAsync(int maxRows)
        {
            return await _repository.Context.Mails
                 .Include(x => x.Attachments)
                 .Where(x => x.Status == Contracts.Enums.MailStatus.Pending)
                 .OrderBy(x => x.CreatedDate)
                 .Take(maxRows)
                 .ToListAsync();
        }
    }
}
