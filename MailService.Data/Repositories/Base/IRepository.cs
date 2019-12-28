using MailService.Domain.Base;
using System.Threading.Tasks;

namespace MailService.Data.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : EntityBase, new()
    {
        MailServiceContext Context { get; }
        void Add(TEntity entity);
        Task SaveChangesAsync();
    }
}