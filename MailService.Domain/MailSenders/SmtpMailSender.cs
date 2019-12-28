using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Domain.MailSenders
{
    public class SmtpMailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _client;
        private readonly ILogger<SmtpMailSender> _logger;

        public SmtpMailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass, bool enableSsl, ILogger<SmtpMailSender> logger)
        {
            _client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = enableSsl,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass)
            };
            _logger = logger;
        }

        public async Task SendMailAsync(string fromAddreess, List<string> toAdresses, string subject, string body, bool isBodyHtml)
        {

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(fromAddreess);

            foreach (var toAddress in toAdresses)
            {
                mailMessage.To.Add(toAddress);
            }

            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            await _client.SendMailAsync(mailMessage);
        }
    }
}
