using MediatR;

namespace MailService.Common.Bus.Event
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
           where TEvent : IEvent
    {
    }
}
