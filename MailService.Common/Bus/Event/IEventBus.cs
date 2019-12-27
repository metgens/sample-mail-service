using System.Threading.Tasks;

namespace MailService.Common.Bus.Event
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}