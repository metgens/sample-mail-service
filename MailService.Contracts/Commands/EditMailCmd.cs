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
        /// <summary>
        /// Id of mail to edit
        /// </summary>
        public Guid MailId { get; private set; }
        /// <summary>
        /// Sender mail address
        /// </summary>
        public string From { get; }
        /// <summary>
        /// Collection of recipients' mail addresses. Collection is replaced with this value, not updated.
        /// </summary>
        public List<string> To { get; }
        /// <summary>
        /// Mail subject
        /// </summary>
        public string Subject { get; }
        /// <summary>
        /// Body of mail. It can be plain text or html
        /// </summary>
        public string Body { get; }
        /// <summary>
        /// When body is html it should be set true
        /// </summary>
        public bool IsBodyHtml { get; }
        /// <summary>
        /// Mail priority, possible values: 'Normal', 'Low', 'High'. Defualt is 'Normal'
        /// </summary>
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
            RuleFor(x => x.MailId).NotEmpty();
            RuleFor(x => x.From).EmailAddress();
            RuleForEach(x => x.To).EmailAddress();
            RuleFor(x => x.Priority).IsEnumName(typeof(CustomMailPriority), caseSensitive: false);
        }
    }
}