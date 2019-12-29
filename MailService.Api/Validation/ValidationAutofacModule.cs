using Autofac;
using FluentValidation;
using MailService.Contracts.Commands;

namespace MailService.Common.AutofacModules
{
    public class ValidationAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateMailCmdValidator).Assembly)
                            .AsClosedTypesOf(typeof(IValidator<>))
                            .AsImplementedInterfaces();

        }
    }
}
