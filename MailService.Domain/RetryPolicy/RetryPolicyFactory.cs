using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.RetryPolicy
{
    public interface IRetryPolicyFactory
    {
        IAsyncPolicy GetForSendingMails();
    }

    public class RetryPolicyFactory : IRetryPolicyFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RetryPolicyFactory> _logger;

        public RetryPolicyFactory(IConfiguration configuration, ILogger<RetryPolicyFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IAsyncPolicy GetForSendingMails()
        {
            if (!int.TryParse(_configuration["MailSender:RetryNo"], out int retryNo))
            {
                _logger.LogWarning("Mail sender retry No not set in appsettings 'MailSender:RetryNo");
                retryNo = 0;
            }
            if (!int.TryParse(_configuration["MailSender:RetryIntervalInSeconds"], out int retryInterval))
            {
                _logger.LogWarning("Mail sender retry No not set in appsettings 'MailSender:RetryIntervalInSeconds");
                retryInterval = 1;
            }

            return Policy.Handle<Exception>().WaitAndRetryAsync(retryNo, retryAttempt => TimeSpan.FromSeconds(retryAttempt * retryInterval));
        }

    }
}
