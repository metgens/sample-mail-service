using MediatR;

namespace MailService.Common.Bus.Command
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
