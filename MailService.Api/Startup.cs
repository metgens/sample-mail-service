using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MailService.Api.Exceptions;
using MailService.Api.Logging;
using MailService.Api.Validation;
using MailService.Common.AutofacModules;
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
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //EF
            services.AddDbContext<MailServiceContext>(options =>
                options.UseSqlServer(Configuration["MailServiceDB:ConnectionString"]));

            //MEDIATR
            services.AddMediatR(typeof(CreateMailCmdHandler).Assembly); //domain
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingCommandsBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingQueriesBehavior<,>));

            //AUTOMAPPER
            services.AddAutoMapper(typeof(GetAllMailsQueryHandler).Assembly); //domain

            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Mail Service Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                xmlFile = $"MailService.Contracts.xml";
                xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OrderActionsBy(description => description.HttpMethod);
                c.DescribeAllEnumsAsStrings();
            });


            //AUTOFAC
            var builder = new ContainerBuilder();
            services.AddSingleton(Configuration);
            builder.Populate(services);
            builder.RegisterModule(new DataAutofacModule());
            builder.RegisterModule(new MediatrBusAutofacModule());
            builder.RegisterModule(new MailSenderAutofacModule());
            builder.RegisterModule(new ValidationAutofacModule());
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
