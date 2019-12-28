using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MailService.Data
{
    public class MigrationsContextFactory : IDesignTimeDbContextFactory<MailServiceContext>
    {
        public MailServiceContext CreateDbContext(string[] args)
        {
            var config = GetConfig();

            var builder = new DbContextOptionsBuilder<MailServiceContext>();
            builder.UseSqlServer(config["MailServiceDB:ConnectionString"], x => x.MigrationsHistoryTable("__EFMigrationsHistory", MailServiceContext.DbSchema));
            return new MailServiceContext(builder.Options);
        }

        private IConfigurationRoot GetConfig()
        {
            var configbuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Migrations.json", false);

            configbuilder.Build();
            var config = configbuilder.Build();
            return config;
        }

    }
}
