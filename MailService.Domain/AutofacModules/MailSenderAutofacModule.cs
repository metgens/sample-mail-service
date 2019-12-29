using Autofac;
using MailService.Domain.MailSenders;
using MailService.Domain.RetryPolicy;
using System.Reflection;

namespace MailService.Data.AutofacModules
{
    public class MailSenderAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SmtpMailSenderFactory>().As<IMailSenderFactory>();
            builder.RegisterType<RetryPolicyFactory>().As<IRetryPolicyFactory>();
            builder.RegisterType<MailSenderFacade>().As<IMailSenderFacade>();
        }
    }
}
