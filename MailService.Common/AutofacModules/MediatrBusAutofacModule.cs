using Autofac;
using MailService.Common.Bus.Command;
using MailService.Common.Bus.Event;
using MailService.Common.Bus.Query;
using System.Reflection;

namespace MailService.Common.AutofacModules
{
    public class MediatrBusAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandBus>().AsImplementedInterfaces();
            builder.RegisterType<QueryBus>().AsImplementedInterfaces();
            builder.RegisterType<EventBus>().AsImplementedInterfaces();

        }
    }
}
