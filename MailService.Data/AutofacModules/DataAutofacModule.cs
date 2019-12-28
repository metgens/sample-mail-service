using Autofac;
using System.Reflection;

namespace MailService.Data.AutofacModules
{
    public class DataAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(thisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

        }
    }
}
