using MailService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Data
{
    public class MailServiceContext : DbContext
    {
        internal const string DbSchema = "ms";

        public MailServiceContext(DbContextOptions<MailServiceContext> options)
            : base(options)
        { }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailAttachment> MailAttachments  { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DbSchema);
            SetupPrimaryKeys(modelBuilder);
            SetupForeignKeys(modelBuilder);
            SetupEnums(modelBuilder);
        }


        private void SetupPrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>().HasKey(x => new { x.Id });
            modelBuilder.Entity<MailAttachment>().HasKey(x => new { x.Id });
           
        }

        private void SetupForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>()
                          .HasMany<MailAttachment>()
                          .WithOne(x => x.Mail);
        }

        private void SetupEnums(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasConversion<string>();
                entity.Property(e => e.Priority)
                    .HasConversion<string>();
            });
        }

    }

}
