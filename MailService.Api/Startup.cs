using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MailService.Common.AutofacModules;
using MailService.Common.Bus.Behaviors;
using MailService.Data;
using MailService.Data.AutofacModules;
using MailService.Domain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //EF
            services.AddDbContext<MailServiceContext>(options =>
                options.UseSqlServer(Configuration["MailServiceDB:ConnectionString"]));

            //MEDIATR
            services.AddMediatR(typeof(CreateMailCmdHandler).Assembly); //domain
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingCommandsBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingQueriesBehavior<,>));

            //AUTOMAPPER
            services.AddAutoMapper(typeof(GetAllMailsQueryHandler).Assembly); //domain

            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Mail.Service API", Version = "v1" });
            });

            //AUTOFAC
            var builder = new ContainerBuilder();
            services.AddSingleton(Configuration);
            builder.Populate(services);
            builder.RegisterModule(new DataAutofacModule());
            builder.RegisterModule(new MediatrBusAutofacModule());
            builder.RegisterModule(new MailSenderAutofacModule());
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
