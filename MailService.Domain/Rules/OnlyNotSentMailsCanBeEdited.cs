using MailService.Common.Exceptions;
using MailService.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.Rules
{
    public class OnlyNotSentMailsCanBeEdited : IBusinessRule
    {
        public OnlyNotSentMailsCanBeEdited(Mail mail)
        {
            _mail = mail;
        }

        private Mail _mail;

        public bool IsBroken() => _mail.Status == MailStatus.Sent;

        public string Message => $"Editing not possible for 'sent' mails. Current mail status: '{_mail.Status}'";
    }
}
