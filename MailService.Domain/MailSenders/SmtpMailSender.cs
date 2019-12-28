using MailService.Contracts.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
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

        public async Task SendMailAsync(Mail mail)
        {

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(mail.From);

            foreach (var toAddress in mail.To)
            {
                mailMessage.To.Add(toAddress);
            }

            mailMessage.Body = mail.Body;
            mailMessage.Subject = mail.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = mail.Priority.ToMailPriority();

            if (!(mail.Attachments is null))
            {
                foreach (var attachment in mail.Attachments)
                {
                    var attachmentToSend = Attachment.CreateAttachmentFromString(attachment.Content, attachment.Name,
                        Encoding.GetEncoding(attachment.Encoding), attachment.MediaType);
                    mailMessage.Attachments.Add(attachmentToSend);
                }
            }

            await _client.SendMailAsync(mailMessage);
        }
    }
}
