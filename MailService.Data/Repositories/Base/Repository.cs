using MailService.Domain.Base;
using System;
using System.Threading.Tasks;

namespace MailService.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        public MailServiceContext Context { get; private set; } 

        public Repository(MailServiceContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
