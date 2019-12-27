using MediatR;

namespace MailService.Common.Bus.Query
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
