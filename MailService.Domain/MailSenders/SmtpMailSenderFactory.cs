using MailService.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.MailSenders
{
    public class SmtpMailSenderFactory : IMailSenderFactory
    {
        private IConfiguration _configuration;
        private ILogger<SmtpMailSender> _logger;

        public SmtpMailSenderFactory(IConfiguration configuration, ILogger<SmtpMailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IMailSender GetMailSender()
        {
            var smtpHost = _configuration["MailSender:Smtp:Host"];
            if (smtpHost == null)
                throw new AppException("Error when sending an email", "Smtp host was not set in appsettings 'MailSender:SmtpHost'", ErrorCode.InternalServerError);

            if (!int.TryParse(_configuration["MailSender:Smtp:Port"], out int smtpPort))
                smtpPort = 587;

            var smtpUser = _configuration["MailSender:Smtp:User"];
            if (smtpUser == null)
                throw new AppException("Error when sending an email", "Smtp user was not set in appsettings 'MailSender:SmtpUser'", ErrorCode.InternalServerError);

            var smtpPass = _configuration["MailSender:Smtp:Pass"];
            if (smtpPass == null)
                throw new AppException("Error when sending an email", "Smtp password was not set in appsettings 'MailSender:SmtpPass'", ErrorCode.InternalServerError);

            if (!bool.TryParse(_configuration["MailSender:Smtp:EnableSsl"], out bool enableSsl))
                throw new AppException("Error when sending an email", "Smtp password was not set in appsettings 'MailSender:EnableSsl'", ErrorCode.InternalServerError);

            return new SmtpMailSender(smtpHost, smtpPort, smtpUser, smtpPass, enableSsl, _logger);
        }

    }
}
