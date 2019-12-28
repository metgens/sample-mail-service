using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Data
{
    public class MailServiceContextFactory
    {
        public static MailServiceContext CreateDbContext(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<MailServiceContext>();
            builder.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", MailServiceContext.DbSchema));
            return new MailServiceContext(builder.Options);
        }

    }
}
