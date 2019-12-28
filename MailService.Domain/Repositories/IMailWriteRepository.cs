using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Domain.Repositories
{
    public interface IMailWriteRepository
    {
        Task<Mail> GetAsync(Guid id);
        Task<List<Mail>> GetPendingAsync(int maxRows);
        void Add(Mail mail);
        Task SaveChangesAsync();
    }
}
