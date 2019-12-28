using Autofac;
using MailService.Domain.MailSenders;
using System.Reflection;

namespace MailService.Data.AutofacModules
{
    public class MailSenderAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SmtpMailSenderFactory>().As<IMailSenderFactory>();
        }
    }
}
