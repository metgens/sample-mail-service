using System.Threading.Tasks;

namespace MailService.Common.Bus.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}
