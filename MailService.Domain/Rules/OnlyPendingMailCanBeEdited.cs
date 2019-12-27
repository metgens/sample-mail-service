using MailService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.Rules
{
    public class OnlyPendingMailCanBeEdited : IBusinessRule
    {
        public OnlyPendingMailCanBeEdited(Mail mail)
        {
            _mail = mail;
        }

        private Mail _mail;

        public bool IsBroken() => _mail.Status != Contracts.Enums.MailStatus.Pending;

        public string Message => $"Mail editing possible only for 'pending' mails. Current mail status: '{_mail.Status}'";
    }
}
