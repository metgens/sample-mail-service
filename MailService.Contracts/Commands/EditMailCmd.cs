using FluentValidation;
using MailService.Contracts.Commands.Base;
using MailService.Contracts.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class EditMailCmd : CommandBase
    {
        public Guid MailId { get; private set; }
        public string From { get; }
        public List<string> To { get; }
        public string Subject { get; }
        public string Body { get; }
        public bool IsBodyHtml { get; }
        public string Priority { get; }

        [JsonConstructor]
        public EditMailCmd(Guid mailId, string from, List<string> to, string subject, string body, bool isBodyHtml,
            string priority = null)
        {
            MailId = mailId;
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
            Priority = priority;
        }

        public void SetMailId(Guid mailId)
        {
            MailId = mailId;
        }
    }

    public class EditMailCmdValidator : AbstractValidator<EditMailCmd>
    {
        public EditMailCmdValidator()
        {
            RuleFor(x => x.From).EmailAddress();
            RuleForEach(x => x.To).EmailAddress();
            RuleFor(x => x.Priority).IsEnumName(typeof(CustomMailPriority), caseSensitive: false);
        }
    }
}