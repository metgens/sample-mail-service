using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Domain.Repositories
{
    public interface IMailRepository
    {
        Task<Mail> GetAsync(Guid id);
        Task AddAsync(Mail mail);
        Task UpdateAsync(Mail mail);
        Task SaveChangesAsync();
    }
}
