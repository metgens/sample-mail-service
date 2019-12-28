using MailService.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MailService.Domain.MailSenders
{
    public static class SmtpMailPriorityExtensions
    {
        public static MailPriority ToMailPriority(this CustomMailPriority cusomMailPriority)
        {
            switch (cusomMailPriority)
            {
                case CustomMailPriority.Normal:
                    return MailPriority.Normal;
                case CustomMailPriority.Low:
                    return MailPriority.Low;
                case CustomMailPriority.High:
                    return MailPriority.High;
                default:
                    return MailPriority.Normal;
            }
        }
    }
}
