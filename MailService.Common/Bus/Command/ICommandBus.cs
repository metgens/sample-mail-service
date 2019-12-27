using System.Threading.Tasks;

namespace MailService.Common.Bus.Command
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
